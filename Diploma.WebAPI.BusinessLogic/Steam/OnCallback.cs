using System.Security.Cryptography;
using Diploma.WebAPI.DataAccess;
using SteamKit2;
using SteamKit2.GC.Dota.Internal;

namespace Diploma.WebAPI.BusinessLogic.Steam;

public partial class SteamGameClient
{
    private void OnGCWelcome(GCWelcomeCallback callback)
    {
        Console.WriteLine($"GC welcoming us, version {callback.Version}");
    }
    
    private void OnConnectionStatus(ConnectionStatusCallback callback)
    {
        Console.WriteLine($"Connection status: {callback.result.status}");
    }

    private void OnPracticeLobbySnapshot(PracticeLobbySnapshotCallback callback)
    {
        if (callback.OldLobby == null)
        {
            MoveToPool();
        }

        Console.WriteLine($"Lobby was updated: {callback.Lobby.state} {callback.Lobby.match_outcome}");
    }

    private void OnPracticeLobbyLeaveCallback(PracticeLobbyLeaveCallback callback)
    {
        Console.WriteLine($"Match was ended {callback.MatchOutcome} {callback.MatchId}");
        using var dbContext = new AppDbContext();

        var match = dbContext.Matches.Single(x => x.Id == MatchId);

        if (callback.MatchOutcome == EMatchOutcome.k_EMatchOutcome_RadVictory)
        {
            match.ParticipantAScore++;
        }
        else
        {
            match.ParticipantBScore++;
        }

        dbContext.SaveChanges();
    }

    private void OnUnhandledDotaGC(UnhandledDotaGCCallback callback)
    {
        Console.WriteLine($"Unhandled message {callback.Message.MsgType}");
    }

    private void OnConnected(SteamClient.ConnectedCallback callback)
    {
        Console.WriteLine("Connected to Steam! Logging in '{0}'...", "jumpyl1on");

        byte[]? sentryHash = null;
        if (File.Exists("sentry.bin"))
        {
            var sentryFile = File.ReadAllBytes("sentry.bin");
            sentryHash = CryptoHelper.SHAHash(sentryFile);
        }

        _steamUser.LogOn(new SteamUser.LogOnDetails
        {
            Username = "jumpyl1on",
            Password = File.ReadAllText("file.txt"),
            AuthCode = null,
            TwoFactorCode = null,
            SentryFileHash = sentryHash,
        });
    }

    private void OnLoggedOn(SteamUser.LoggedOnCallback callback)
    {
        if (callback.Result == EResult.AccountLogonDenied)
        {
            Console.WriteLine("This account is SteamGuard protected!");

            Console.Write("Please enter your 2 factor auth code from your authenticator app: ");
            //var twoFactorAuth = Console.ReadLine();
            return;
        }

        if (callback.Result == EResult.AccountLoginDeniedNeedTwoFactor)
        {
            Console.WriteLine("This account is SteamGuard protected!");

            Console.Write("Please enter the auth code sent to the email at {0}: ", callback.EmailDomain);
            //var authCode = Console.ReadLine();
            return;
        }

        if (callback.Result != EResult.OK)
        {
            Console.WriteLine("Unable to logon to Steam: {0} / {1}", callback.Result, callback.ExtendedResult);

            return;
        }

        Console.WriteLine("Successfully logged on!");

        Start();
    }

    private void OnDisconnected(SteamClient.DisconnectedCallback callback)
    {
        Console.WriteLine("Disconnected from Steam");

        if (!File.Exists("sentry.bin"))
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));

            Client.Connect();
        }
        else
        {
            _timer?.Dispose();
            _timer = null;
        }
    }

    private void OnLoggedOff(SteamUser.LoggedOffCallback callback)
    {
        Console.WriteLine("Logged off of Steam: {0}", callback.Result);
    }

    private void OnMachineAuth(SteamUser.UpdateMachineAuthCallback callback)
    {
        Console.WriteLine("Updating sentry file...");

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

        _steamUser.SendMachineAuthResponse(new SteamUser.MachineAuthDetails
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
}