using System.Security.Cryptography;
using SteamBot;
using SteamKit2;
using SteamKit2.GC;
using SteamKit2.GC.Dota.Internal;
using SteamKit2.Internal;

DebugLog.AddListener( new SimpleConsoleDebugListener() );
DebugLog.Enabled = true;

string authCode = null, twoFactorAuth = null;

var steamClient = new SteamClient();
steamClient.AddHandler(new SteamGameClient());

var manager = new CallbackManager(steamClient);

var steamUser = steamClient.GetHandler<SteamUser>();
var gameClient = steamClient.GetHandler<SteamGameClient>();

manager.Subscribe<SteamClient.ConnectedCallback>(OnConnected);
manager.Subscribe<SteamClient.DisconnectedCallback>(OnDisconnected);

manager.Subscribe<SteamUser.LoggedOnCallback>(OnLoggedOn);
manager.Subscribe<SteamUser.LoggedOffCallback>(OnLoggedOff);
manager.Subscribe<SteamUser.UpdateMachineAuthCallback>(OnMachineAuth);

manager.Subscribe<SteamGameClient.ConnectionStatusCallback>(OnConnectionStatus);
manager.Subscribe<SteamGameClient.GCWelcomeCallback>(OnGCWelcome);
manager.Subscribe<SteamGameClient.UnhandledDotaGCCallback>(OnUnhandledDotaGC);
manager.Subscribe<SteamGameClient.PracticeLobbySnapshotCallback>(OnPracticeLobbySnapshot);

Console.WriteLine("Connecting to Steam...");

steamClient.Connect();

var timer = new Timer(
    _ => manager.RunCallbacks(),
    null,
    0,
    1000);

var isRunning = true;
while (isRunning)
{
    var command = Console.ReadLine();
    switch (command)
    {
        case "start":
        {
            gameClient.Start();
            break;
        }
        case "create lobby":
        {
            gameClient.CreateLobby("", new CMsgPracticeLobbySetDetails
            {
                game_mode = (uint)DOTA_GameMode.DOTA_GAMEMODE_AP,
                game_name = "STEAMKIT2.DOTA.LOBBY",
                game_version = DOTAGameVersion.GAME_VERSION_CURRENT,
                allow_cheats = true,
                allow_spectating = true
            });
            break;
        }
        case "move to pool":
        {
            gameClient.MoveToPool();
            break;
        }
        case "invite me":
        {
            gameClient.InviteToLobby(76561198083807815);
            break;
        }
        case "leave lobby":
        {
            gameClient.LeaveLobby();
            break;
        }
        case "start game":
        {
            gameClient.LaunchLobby();
            break;
        }
        case "abandon game":
        {
            gameClient.LeaveGame();
            break;
        }
        case "stop":
        {
            gameClient.Stop();
            break;
        }
        case "exit":
        {
            steamUser.LogOff();
            break;
        }
    }
}

void OnConnectionStatus(SteamGameClient.ConnectionStatusCallback callback)
{
    Console.WriteLine($"Connection status: {callback.result.status}");
}

void OnGCWelcome(SteamGameClient.GCWelcomeCallback callback)
{
    Console.WriteLine($"GC welcoming us, version {callback.Version}");
}

void OnPracticeLobbySnapshot(SteamGameClient.PracticeLobbySnapshotCallback callback)
{
    Console.WriteLine(
        $"Lobby was updated: {callback.lobby.state} {callback.lobby.game_state} {callback.lobby.match_outcome}");
}

void OnUnhandledDotaGC(SteamGameClient.UnhandledDotaGCCallback callback)
{
    Console.WriteLine($"Unhandled message {callback.Message.MsgType}");
}

void OnConnected(SteamClient.ConnectedCallback callback)
{
    Console.WriteLine("Connected to Steam! Logging in '{0}'...", "jumpyl1on");

    byte[]? sentryHash = null;
    if (File.Exists("sentry.bin"))
    {
        var sentryFile = File.ReadAllBytes("sentry.bin");
        sentryHash = CryptoHelper.SHAHash(sentryFile);
    }

    steamUser.LogOn(new SteamUser.LogOnDetails
    {
        Username = "jumpyl1on",
        Password = File.ReadAllText("file.txt"),
        AuthCode = authCode,
        TwoFactorCode = twoFactorAuth,
        SentryFileHash = sentryHash,
    });
}

void OnDisconnected(SteamClient.DisconnectedCallback callback)
{
    Console.WriteLine("Disconnected from Steam");

    if (!File.Exists("sentry.bin"))
    {
        Thread.Sleep(TimeSpan.FromSeconds(5));

        steamClient.Connect();
    }
    else
    {
        isRunning = false;
    }
}

void OnLoggedOn(SteamUser.LoggedOnCallback callback)
{
    bool isSteamGuard = callback.Result == EResult.AccountLogonDenied;
    bool is2FA = callback.Result == EResult.AccountLoginDeniedNeedTwoFactor;

    if (isSteamGuard || is2FA)
    {
        Console.WriteLine("This account is SteamGuard protected!");

        if (is2FA)
        {
            Console.Write("Please enter your 2 factor auth code from your authenticator app: ");
            twoFactorAuth = Console.ReadLine();
        }
        else
        {
            Console.Write("Please enter the auth code sent to the email at {0}: ", callback.EmailDomain);
            authCode = Console.ReadLine();
        }

        return;
    }

    if (callback.Result != EResult.OK)
    {
        Console.WriteLine("Unable to logon to Steam: {0} / {1}", callback.Result, callback.ExtendedResult);

        isRunning = false;
        return;
    }

    Console.WriteLine("Successfully logged on!");
}

void OnLoggedOff(SteamUser.LoggedOffCallback callback)
{
    Console.WriteLine("Logged off of Steam: {0}", callback.Result);
}

void OnMachineAuth(SteamUser.UpdateMachineAuthCallback callback)
{
    Console.WriteLine("Updating sentryfile...");

    // write out our sentry file
    // ideally we'd want to write to the filename specified in the callback
    // but then this sample would require more code to find the correct sentry file to read during logon
    // for the sake of simplicity, we'll just use "sentry.bin"

    int fileSize;
    byte[] sentryHash;
    using (var fs = File.Open("sentry.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite))
    {
        fs.Seek(callback.Offset, SeekOrigin.Begin);
        fs.Write(callback.Data, 0, callback.BytesToWrite);
        fileSize = (int)fs.Length;

        fs.Seek(0, SeekOrigin.Begin);
        using var sha = SHA1.Create();
        sentryHash = sha.ComputeHash(fs);
    }

    // inform the steam servers that we're accepting this sentry file
    steamUser.SendMachineAuthResponse(new SteamUser.MachineAuthDetails
    {
        JobID = callback.JobID,

        FileName = callback.FileName,

        BytesWritten = callback.BytesToWrite,
        FileSize = fileSize,
        Offset = callback.Offset,

        Result = EResult.OK,
        LastError = 0,

        OneTimePassword = callback.OneTimePassword,

        SentryFileHash = sentryHash,
    });

    Console.WriteLine("Done!");
}