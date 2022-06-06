using ProtoBuf;
using SteamKit2;
using SteamKit2.GC;
using SteamKit2.GC.Dota.Internal;
using SteamKit2.Internal;

namespace Diploma.WebAPI.BusinessLogic.Steam;

public partial class SteamGameClient : ClientMsgHandler, IDisposable
{
    public Guid MatchId { get; set; }

    private readonly CallbackManager _callbackManager;
    private readonly SteamUser _steamUser;
    
    private readonly uint _appId;
    private readonly ESourceEngine _engine;
    private CSODOTALobby? _lobby;

    private Timer? _timer;

    public SteamGameClient(SteamClient steamClient)
    {
        _callbackManager = new CallbackManager(steamClient);
        
        _callbackManager.Subscribe<SteamClient.ConnectedCallback>(OnConnected);
        _callbackManager.Subscribe<SteamClient.DisconnectedCallback>(OnDisconnected);

        _steamUser = steamClient.GetHandler<SteamUser>()!;
        _callbackManager.Subscribe<SteamUser.LoggedOnCallback>(OnLoggedOn);
        _callbackManager.Subscribe<SteamUser.LoggedOffCallback>(OnLoggedOff);
        _callbackManager.Subscribe<SteamUser.UpdateMachineAuthCallback>(OnUpdateMachineAuth);

        _callbackManager.Subscribe<ClientWelcomeCallback>(OnClientWelcome);
        _callbackManager.Subscribe<ClientConnectionStatusCallback>(OnClientConnectionStatus);
        _callbackManager.Subscribe<CacheSubscribedCallback>(OnCacheSubscribed);
        _callbackManager.Subscribe<CacheUnsubscribedCallback>(OnCacheUnsubscribedCallback);
        _callbackManager.Subscribe<UpdateMultipleCallback>(OnUpdateMultiple);
        _callbackManager.Subscribe<UnknownCallback>(OnUnknown);
        

        _engine = ESourceEngine.k_ESE_Source2;
        _appId = 570;

        steamClient.Connect();

        _timer = new Timer(
            _ => _callbackManager.RunCallbacks(),
            null,
            TimeSpan.Zero,
            TimeSpan.FromSeconds(1));
    }

    public void CreateLobby(string passKey, CMsgPracticeLobbySetDetails details)
    {
        Console.WriteLine($"Creating lobby {details.game_name}");

        var msg = new ClientGCMsgProtobuf<CMsgPracticeLobbyCreate>((uint)EDOTAGCMsg.k_EMsgGCPracticeLobbyCreate);

        msg.Body.pass_key = passKey;
        msg.Body.lobby_details = details;
        msg.Body.lobby_details.pass_key = passKey;
        msg.Body.lobby_details.visibility = DOTALobbyVisibility.DOTALobbyVisibility_Public;

        if (string.IsNullOrWhiteSpace(msg.Body.search_key))
        {
            msg.Body.search_key = "";
        }

        Send(msg);
    }

    public void InviteToLobby(ulong steamId)
    {
        Console.WriteLine($"Inviting user with id {steamId}");
        
        var msg = new ClientGCMsgProtobuf<CMsgInviteToLobby>((uint)EGCBaseMsg.k_EMsgGCInviteToLobby);
        
        msg.Body.steam_id = steamId;
        
        Send(msg);
    }

    public void KickFromTeam(uint accountId)
    {
        var kick = new ClientGCMsgProtobuf<CMsgPracticeLobbyKickFromTeam>(
            (uint)EDOTAGCMsg.k_EMsgGCPracticeLobbyKickFromTeam);
        kick.Body.account_id = accountId;
        Send(kick);
    }

    public void LaunchLobby()
    {
        Console.WriteLine("Starting game");

        var msg = new ClientGCMsgProtobuf<CMsgPracticeLobbyLaunch>((uint)EDOTAGCMsg.k_EMsgGCPracticeLobbyLaunch); 
        
        Send(msg);
    }

    public void LeaveGame()
    {
        Console.WriteLine("Abandon current game");
        
        var msg = new ClientGCMsgProtobuf<CMsgAbandonCurrentGame>((uint)EDOTAGCMsg.k_EMsgGCAbandonCurrentGame);
        
        Send(msg);
    }

    public void Dispose()
    {
        Console.WriteLine("Stop playing dota 2");

        LeaveGame();

        Thread.Sleep(1000);

        LeaveLobby();

        Thread.Sleep(1000);

        var msg = new ClientMsgProtobuf<CMsgClientGamesPlayed>(EMsg.ClientGamesPlayed);

        Client.Send(msg);

        Thread.Sleep(5000);

        _steamUser.LogOff();

        while (_timer != null)
        {
        }
    }

    public override void HandleMsg(IPacketMsg packetMsg)
    {
        if (packetMsg.MsgType != EMsg.ClientFromGC)
        {
            return;
        }

        var msg = new ClientMsgProtobuf<CMsgGCClient>(packetMsg);

        if (msg.Body.appid != _appId)
        {
            return;
        }

        var gcMsg = GetPacketGCMsg(msg.Body.msgtype, msg.Body.payload);

        var messageMap = new Dictionary<uint, Action<IPacketGCMsg>>
        {
            { (uint)EGCBaseClientMsg.k_EMsgGCClientWelcome, HandleClientWelcome },
            { (uint)EGCBaseClientMsg.k_EMsgGCClientConnectionStatus, HandleClientConnectionStatus },
            { (uint)ESOMsg.k_ESOMsg_CacheSubscribed, HandleCacheSubscribed },
            { (uint)ESOMsg.k_ESOMsg_CacheUnsubscribed, HandleCacheUnsubscribed },
            { (uint)ESOMsg.k_ESOMsg_UpdateMultiple, HandleUpdateMultiple }
        };

        if (!messageMap.TryGetValue(gcMsg.MsgType, out var action))
        {
            Client.PostCallback(new UnknownCallback(gcMsg));
            return;
        }

        action(gcMsg);
    }

    private void HandleClientWelcome(IPacketGCMsg packetMsg)
    {
        _lobby = null;

        var msg = new ClientGCMsgProtobuf<CMsgClientWelcome>(packetMsg);

        Client.PostCallback(new ClientWelcomeCallback(msg.Body));
    }

    private void HandleClientConnectionStatus(IPacketGCMsg obj)
    {
        var msg = new ClientGCMsgProtobuf<CMsgConnectionStatus>(obj);

        Client.PostCallback(new ClientConnectionStatusCallback(msg.Body));
    }

    private void HandleCacheSubscribed(IPacketGCMsg packetMsg)
    {
        var msg = new ClientGCMsgProtobuf<CMsgSOCacheSubscribed>(packetMsg);

        foreach (var unused in msg.Body.objects.Where(x => x.type_id == 2004))
        {
            Client.PostCallback(new CacheSubscribedCallback(msg.Body));
        }
    }

    private void HandleCacheUnsubscribed(IPacketGCMsg packetMsg)
    {
        var msg = new ClientGCMsgProtobuf<CMsgSOCacheUnsubscribed>(packetMsg);

        if (_lobby == null || msg.Body.owner_soid.id != _lobby.lobby_id)
        {
            return;
        }

        Client.PostCallback(new CacheUnsubscribedCallback(_lobby));

        _lobby = null;
    }

    private void HandleUpdateMultiple(IPacketGCMsg packetMsg)
    {
        var msg = new ClientGCMsgProtobuf<CMsgSOMultipleObjects>(packetMsg);

        foreach (var obj in msg.Body.objects_modified.Where(obj => obj.type_id == 2004))
        {
            using var stream = new MemoryStream(obj.object_data);
            {
                var lobby = Serializer.Deserialize<CSODOTALobby>(stream);

                var oldLobby = _lobby;
                _lobby = lobby;

                Client.PostCallback(new UpdateMultipleCallback(oldLobby, lobby));
            }
        }
    }
    
    private void Launch()
    {
        Console.WriteLine("Launching Dota 2");

        var msg = new ClientMsg<MsgClientAppUsageEvent>();
        
        msg.Body.AppUsageEvent = EAppUsageEvent.GameLaunch;
        msg.Body.GameID = new GameID
        {
            AppID = _appId,
            AppType = GameID.GameType.App
        };
        
        Client.Send(msg);

        var msgProtobuf = new ClientMsgProtobuf<CMsgClientGamesPlayed>(EMsg.ClientGamesPlayedWithDataBlob);
        
        msgProtobuf.Body.games_played.Add(new CMsgClientGamesPlayed.GamePlayed
        {
            game_id = _appId,
            game_extra_info = "Dota 2",
            game_data_blob = null,
            streaming_provider_id = 0,
            game_flags = (uint)_engine,
            owner_id = Client.SteamID!.AccountID
        });
        
        msgProtobuf.Body.client_os_type = (uint)EOSType.Windows10;
        
        Client.Send(msgProtobuf);

        SayHello();

        Thread.Sleep(5000);
    }

    private void MoveToPool()
    {
        Console.WriteLine("Move bot to pool");

        var msg = new ClientGCMsgProtobuf<CMsgPracticeLobbySetTeamSlot>(
            (uint)EDOTAGCMsg.k_EMsgGCPracticeLobbySetTeamSlot);

        msg.Body.team = DOTA_GC_TEAM.DOTA_GC_TEAM_PLAYER_POOL;
        msg.Body.slot = 1;

        Send(msg);
    }
    
    private void LeaveLobby()
    {
        Console.WriteLine("Leaving lobby");

        var msg = new ClientGCMsgProtobuf<CMsgPracticeLobbyLeave>((uint)EDOTAGCMsg.k_EMsgGCPracticeLobbyLeave);

        Send(msg);
    }

    private void SayHello()
    {
        var msg = new ClientGCMsgProtobuf<CMsgClientHello>((uint)EGCBaseClientMsg.k_EMsgGCClientHello);

        msg.Body.client_launcher = PartnerAccountType.PARTNER_NONE;
        msg.Body.engine = _engine;
        msg.Body.secret_key = "";
        msg.Body.client_session_need = (uint)EDOTAGCSessionNeed.k_EDOTAGCSessionNeed_UserInUINeverConnected;

        Send(msg);
    }

    private void Send(IClientGCMsg msg)
    {
        var clientMsg = new ClientMsgProtobuf<CMsgGCClient>(EMsg.ClientToGC);
        
        clientMsg.Body.msgtype = MsgUtil.MakeGCMsg(msg.MsgType, msg.IsProto);
        
        clientMsg.Body.appid = _appId;
        
        clientMsg.Body.payload = msg.Serialize();
        
        Client.Send(clientMsg);
    }

    private static IPacketGCMsg GetPacketGCMsg(uint msg, byte[] data)
    {
        var packetMsg = MsgUtil.GetGCMsg(msg);
        
        if (MsgUtil.IsProtoBuf(msg))
        {
            return new PacketClientGCMsgProtobuf(packetMsg, data);
        }

        return new PacketClientGCMsg(packetMsg, data);
    }
}