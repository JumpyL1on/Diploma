using SteamKit2;
using SteamKit2.GC;
using SteamKit2.GC.Dota.Internal;

namespace SteamBot;

public partial class SteamGameClient
{
    public sealed class CacheUnsubscribed : CallbackMsg
    {
        public CMsgSOCacheUnsubscribed result;

        internal CacheUnsubscribed(CMsgSOCacheUnsubscribed msg)
        {
            result = msg;
        }
    }

    public sealed class ConnectionStatus : CallbackMsg
    {
        public CMsgConnectionStatus result;

        internal ConnectionStatus(CMsgConnectionStatus msg)
        {
            result = msg;
        }
    }
    
    public sealed class PracticeLobbyLeave : CallbackMsg
    {
        public CMsgSOCacheUnsubscribed result;

        internal PracticeLobbyLeave(CMsgSOCacheUnsubscribed msg)
        {
            result = msg;
        }
    }
    
    public sealed class PracticeLobbySnapshot : CallbackMsg
    {
        public CSODOTALobby lobby;
        public CSODOTALobby? oldLobby;

        internal PracticeLobbySnapshot(CSODOTALobby msg, CSODOTALobby? oldLob)
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