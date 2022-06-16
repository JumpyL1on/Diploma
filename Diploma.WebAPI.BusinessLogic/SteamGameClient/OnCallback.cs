using System.Security.Cryptography;
using Diploma.WebAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using SteamKit2;
using SteamKit2.GC.Dota.Internal;

namespace Diploma.WebAPI.BusinessLogic.SteamGameClient;

public partial class SteamGameClient
{
    private void OnClientConnectionStatus(ClientConnectionStatusCallback callback)
    {
        Console.WriteLine($"Client was connected with {callback.Body.status} status");
    }

    private void OnClientWelcome(ClientWelcomeCallback callback)
    {
        Console.WriteLine($"Client was welcomed with {callback.Body.version} version");
    }

    private void OnCacheSubscribed(CacheSubscribedCallback callback)
    {
        Console.WriteLine($"Cache was subscribed: {callback.Body.owner_soid.id}");
        
        MoveToPool();
    }

    private void OnCacheUnsubscribedCallback(CacheUnsubscribedCallback callback)
    {
        Console.WriteLine($"Cache was unsubscribed: {callback.Lobby.match_outcome}");

        var lobby = callback.Lobby;

        using var dbContext = new AppDbContext();

        var match = dbContext.Matches
            .Include(match => match.Tournament)
            .Single(match => match.Id == _matchId);
        
        match.Tournament.FinishedAt = match.FinishedAt = DateTime.UtcNow;

        if (lobby.match_outcome == EMatchOutcome.k_EMatchOutcome_RadVictory)
        {
            match.LeftTeamScore++;
        }
        else
        {
            match.RightTeamScore++;
        }

        dbContext.SaveChanges();
    }

    private void OnUpdateMultiple(UpdateMultipleCallback callback)
    {
        Console.WriteLine($"It was updated with {callback.Lobby.state} {callback.Lobby.match_outcome}");

        foreach (var e in callback.Lobby.all_members)
        {
            switch (e.team)
            {
                case DOTA_GC_TEAM.DOTA_GC_TEAM_GOOD_GUYS when _rightTeam.Contains(e.id):
                case DOTA_GC_TEAM.DOTA_GC_TEAM_BAD_GUYS when _leftTeam.Contains(e.id):
                    KickFromTeam(e.id);
                    break;
            }
        }
    }


    private void OnUnknown(UnknownCallback callback)
    {
        Console.WriteLine($"Unknown callback with {callback.Body.MsgType} type");
    }

    private void OnConnected(SteamClient.ConnectedCallback callback)
    {
        Console.WriteLine("Connected to Steam");

        byte[]? sentryHash = null;
        
        if (File.Exists("sentry.bin"))
        {
            var sentryFile = File.ReadAllBytes("sentry.bin");
            
            sentryHash = CryptoHelper.SHAHash(sentryFile);
        }

        _steamUser.LogOn(new SteamUser.LogOnDetails
        {
            Username = "jumpyl1on",
            Password = "064jGUi#%2",
            AuthCode = null,
            TwoFactorCode = null,
            SentryFileHash = sentryHash,
        });
    }

    private void OnDisconnected(SteamClient.DisconnectedCallback callback)
    {
        Console.WriteLine("Disconnected from Steam");

        if (!File.Exists("sentry.bin"))
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));

            Client.Connect();
        }
    }

    private void OnLoggedOn(SteamUser.LoggedOnCallback callback)
    {
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

        Console.WriteLine("Logged on on Steam");

        Launch();
    }

    private void OnLoggedOff(SteamUser.LoggedOffCallback callback)
    {
        Console.WriteLine("Logged off of Steam: {0}", callback.Result);
    }

    private void OnUpdateMachineAuth(SteamUser.UpdateMachineAuthCallback callback)
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