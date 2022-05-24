using SteamKit2;
using SteamKit2.GC;
using SteamKit2.GC.Dota.Internal;

namespace Diploma.WebAPI.BusinessLogic;

public partial class SteamGameClient
{
    public sealed class CacheUnsubscribedCallback : CallbackMsg
    {
        public CMsgSOCacheUnsubscribed result;

        internal CacheUnsubscribedCallback(CMsgSOCacheUnsubscribed msg)
        {
            result = msg;
        }
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
        public CMsgSOCacheUnsubscribed result;

        internal PracticeLobbyLeaveCallback(CMsgSOCacheUnsubscribed msg)
        {
            result = msg;
        }
    }
    
    public sealed class PracticeLobbySnapshotCallback : CallbackMsg
    {
        public CSODOTALobby lobby;
        public CSODOTALobby? oldLobby;

        internal PracticeLobbySnapshotCallback(CSODOTALobby msg, CSODOTALobby? oldLob)
        {
            lobby = msg;
            oldLobby = oldLob;
        }
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