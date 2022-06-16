using SteamKit2;
using SteamKit2.GC;
using SteamKit2.GC.Dota.Internal;

namespace Diploma.WebAPI.BusinessLogic.SteamGameClient;

public partial class SteamGameClient
{
    public sealed class ClientWelcomeCallback : CallbackMsg
    {
        internal ClientWelcomeCallback(CMsgClientWelcome body)
        {
            Body = body;
        }

        public CMsgClientWelcome Body { get; }
    }

    public sealed class ClientConnectionStatusCallback : CallbackMsg
    {
        internal ClientConnectionStatusCallback(CMsgConnectionStatus body)
        {
            Body = body;
        }

        public CMsgConnectionStatus Body { get; }
    }

    public sealed class CacheSubscribedCallback : CallbackMsg
    {
        internal CacheSubscribedCallback(CMsgSOCacheSubscribed body)
        {
            Body = body;
        }

        public CMsgSOCacheSubscribed Body { get; }
    }

    public sealed class CacheUnsubscribedCallback : CallbackMsg
    {
        internal CacheUnsubscribedCallback(CSODOTALobby lobby)
        {
            Lobby = lobby;
        }

        public CSODOTALobby Lobby { get; }
    }

    public sealed class UpdateMultipleCallback : CallbackMsg
    {
        internal UpdateMultipleCallback(CSODOTALobby? oldLobby, CSODOTALobby lobby)
        {
            OldLobby = oldLobby;
            Lobby = lobby;
        }

        public CSODOTALobby? OldLobby { get; }
        public CSODOTALobby Lobby { get; }
    }

    public sealed class UnknownCallback : CallbackMsg
    {
        internal UnknownCallback(IPacketGCMsg body)
        {
            Body = body;
        }

        public IPacketGCMsg Body { get; }
    }
}