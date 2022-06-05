using SteamKit2;
using SteamKit2.GC;
using SteamKit2.GC.Dota.Internal;

namespace Diploma.WebAPI.BusinessLogic.Steam;

public partial class SteamGameClient
{
    public sealed class CacheUnsubscribedCallback : CallbackMsg
    {
        internal CacheUnsubscribedCallback(CMsgSOCacheUnsubscribed msg)
        {
            Msg = msg;
        }

        public CMsgSOCacheUnsubscribed Msg { get; }
    }

    public sealed class ConnectionStatusCallback : CallbackMsg
    {
        public CMsgConnectionStatus result;

        internal ConnectionStatusCallback(CMsgConnectionStatus msg)
        {
            result = msg;
        }
    }

    public sealed class PracticeLobbyLeaveCallback : CallbackMsg
    {
        internal PracticeLobbyLeaveCallback(CMsgSOCacheUnsubscribed msg, EMatchOutcome matchOutcome)
        {
            Msg = msg;
            MatchOutcome = matchOutcome;
        }

        public CMsgSOCacheUnsubscribed Msg { get; }
        public Guid MatchId { get; set; }
        public EMatchOutcome MatchOutcome { get; }
    }

    public sealed class PracticeLobbySnapshotCallback : CallbackMsg
    {
        internal PracticeLobbySnapshotCallback(CSODOTALobby msg, CSODOTALobby? oldLob)
        {
            Lobby = msg;
            OldLobby = oldLob;
        }

        public CSODOTALobby Lobby { get; }
        public CSODOTALobby? OldLobby { get; }
    }

    public sealed class GCWelcomeCallback : CallbackMsg
    {
        public uint Version;

        internal GCWelcomeCallback(CMsgClientWelcome msg)
        {
            Version = msg.version;
        }
    }

    public sealed class UnhandledDotaGCCallback : CallbackMsg
    {
        internal UnhandledDotaGCCallback(IPacketGCMsg msg)
        {
            Message = msg;
        }

        public IPacketGCMsg Message { get; }
    }
}