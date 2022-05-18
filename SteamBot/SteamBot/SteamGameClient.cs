using ProtoBuf;
using SteamKit2;
using SteamKit2.GC;
using SteamKit2.GC.Dota.Internal;
using SteamKit2.Internal;

namespace SteamBot;

public partial class SteamGameClient : ClientMsgHandler
{
    private readonly uint _appId;
    private readonly ESourceEngine _engine;
    private bool _isRunning;
    private CSODOTALobby? _lobby;

    public SteamGameClient()
    {
        _engine = ESourceEngine.k_ESE_Source2;
        _appId = 570;
    }

    public void Start()
    {
        _isRunning = true;
        var launchEvent = new ClientMsg<MsgClientAppUsageEvent>();
        launchEvent.Body.AppUsageEvent = EAppUsageEvent.GameLaunch;
        launchEvent.Body.GameID = new GameID { AppID = _appId, AppType = GameID.GameType.App };
        Client.Send(launchEvent);
        //UploadRichPresence(RPType.Init);
        var playGame = new ClientMsgProtobuf<CMsgClientGamesPlayed>(EMsg.ClientGamesPlayedWithDataBlob);
        playGame.Body.games_played.Add(new CMsgClientGamesPlayed.GamePlayed
        {
            game_id = _appId,
            game_extra_info = "Dota 2",
            game_data_blob = null,
            streaming_provider_id = 0,
            game_flags = (uint)_engine,
            owner_id = Client.SteamID.AccountID
        });
        playGame.Body.client_os_type = (uint)EOSType.Windows10;
        Client.Send(playGame);
        SayHello();
        Thread.Sleep(5000);
    }

    public void CreateLobby(string passKey, CMsgPracticeLobbySetDetails details)
    {
        var create = new ClientGCMsgProtobuf<CMsgPracticeLobbyCreate>((uint)EDOTAGCMsg.k_EMsgGCPracticeLobbyCreate);
        create.Body.pass_key = passKey;
        create.Body.lobby_details = details;
        create.Body.lobby_details.pass_key = passKey;
        create.Body.lobby_details.visibility = DOTALobbyVisibility.DOTALobbyVisibility_Friends;
        if (string.IsNullOrWhiteSpace(create.Body.search_key))
            create.Body.search_key = "";
        Send(create);
    }

    public void MoveToPool()
    {
        var move = new ClientGCMsgProtobuf<CMsgPracticeLobbySetTeamSlot>((uint)EDOTAGCMsg.k_EMsgGCPracticeLobbySetTeamSlot);
        move.Body.team = DOTA_GC_TEAM.DOTA_GC_TEAM_PLAYER_POOL;
        move.Body.slot = 1;
        Send(move);
    }

    public void InviteToLobby(ulong steam_id)
    {
        var invite = new ClientGCMsgProtobuf<CMsgInviteToLobby>((uint)EGCBaseMsg.k_EMsgGCInviteToLobby);
        invite.Body.steam_id = steam_id;
        Send(invite);
    }

    public void LeaveLobby()
    {
        var leave = new ClientGCMsgProtobuf<CMsgPracticeLobbyLeave>((uint)EDOTAGCMsg.k_EMsgGCPracticeLobbyLeave);
        Send(leave);
    }

    public void LaunchLobby()
    {
        Send(new ClientGCMsgProtobuf<CMsgPracticeLobbyLaunch>((uint)EDOTAGCMsg.k_EMsgGCPracticeLobbyLaunch));
    }

    public void LeaveGame()
    {
        var leave = new ClientGCMsgProtobuf<CMsgAbandonCurrentGame>((uint)EDOTAGCMsg.k_EMsgGCAbandonCurrentGame);
        Send(leave);
    }

    public void Print()
    {
        Console.WriteLine($"{_lobby.state} {_lobby.game_state} {_lobby.match_outcome}");
    }

    public void Stop()
    {
        _isRunning = false;
        var playGame = new ClientMsgProtobuf<CMsgClientGamesPlayed>(EMsg.ClientGamesPlayed);
        Client.Send(playGame);
        //UploadRichPresence(RPType.None);
    }

    public override void HandleMsg(IPacketMsg packetMsg)
    {
        if (packetMsg.MsgType != EMsg.ClientFromGC)
            return;
        
        var msg = new ClientMsgProtobuf<CMsgGCClient>(packetMsg);
        if (msg.Body.appid != _appId)
            return;
        
        var gcMsg = GetPacketGCMsg(msg.Body.msgtype, msg.Body.payload);
        var messageMap = new Dictionary<uint, Action<IPacketGCMsg>>
        {
            { (uint)ESOMsg.k_ESOMsg_Destroy, HandleCacheDestroy },
            { (uint)ESOMsg.k_ESOMsg_CacheSubscribed, HandleCacheSubscribed },
            { (uint)ESOMsg.k_ESOMsg_CacheUnsubscribed, HandleCacheUnsubscribed },
            { (uint)ESOMsg.k_ESOMsg_UpdateMultiple, HandleUpdateMultiple },
            { (uint)EGCBaseClientMsg.k_EMsgGCClientWelcome, HandleWelcome },
            { (uint)EGCBaseClientMsg.k_EMsgGCClientConnectionStatus, HandleConnectionStatus },
        };
        if (!messageMap.TryGetValue(gcMsg.MsgType, out var func))
        {
            Client.PostCallback(new UnhandledDotaGCCallback(gcMsg));
            return;
        }

        func(gcMsg);
    }

    private void HandleConnectionStatus(IPacketGCMsg obj)
    {
        if (!_isRunning)
        {
            Stop();
            return;
        }

        var msg = new ClientGCMsgProtobuf<CMsgConnectionStatus>(obj);
        Client.PostCallback(new ConnectionStatus(msg.Body));
    }

    private void HandleWelcome(IPacketGCMsg msg)
    {
        _lobby = null;
        var wel = new ClientGCMsgProtobuf<CMsgClientWelcome>(msg);
        Client.PostCallback(new GCWelcomeCallback(wel.Body));
        foreach (var cache in wel.Body.outofdate_subscribed_caches)
        foreach (var obj in cache.objects)
            HandleSubscribedType(obj);
        //UploadRichPresence();
    }

    private void HandleCacheDestroy(IPacketGCMsg obj)
    {
        var dest = new ClientGCMsgProtobuf<CMsgSOSingleObject>(obj);
        if (_lobby != null && dest.Body.type_id == 2004)
        {
            _lobby = null;
            Client.PostCallback(new PracticeLobbyLeave(null));
        }
    }

    private void HandleCacheSubscribed(IPacketGCMsg obj)
    {
        var sub = new ClientGCMsgProtobuf<CMsgSOCacheSubscribed>(obj);
        foreach (var cache in sub.Body.objects)
        {
            HandleSubscribedType(cache);
        }
    }

    private void HandleSubscribedType(CMsgSOCacheSubscribed.SubscribedType cache)
    {
        switch (cache.type_id)
        {
            case 2004:
                HandleLobbySnapshot(cache.object_data[0]);
                break;
        }
    }

    private void HandleCacheUnsubscribed(IPacketGCMsg obj)
    {
        var unSub = new ClientGCMsgProtobuf<CMsgSOCacheUnsubscribed>(obj);
        if (_lobby != null && unSub.Body.owner_soid.id == _lobby.lobby_id)
        {
            _lobby = null;
            Client.PostCallback(new PracticeLobbyLeave(unSub.Body));
        }
        else
        {
            Client.PostCallback(new CacheUnsubscribed(unSub.Body));
        }
    }

    private void HandleUpdateMultiple(IPacketGCMsg obj)
    {
        var resp = new ClientGCMsgProtobuf<CMsgSOMultipleObjects>(obj);
        var handled = true;
        foreach (var mObj in resp.Body.objects_modified)
        {
            if (mObj.type_id == 2004)
            {
                HandleLobbySnapshot(mObj.object_data);
            }
            else
            {
                handled = false;
            }
        }

        if (!handled)
        {
            Client.PostCallback(new UnhandledDotaGCCallback(obj));
        }
    }

    private void HandleLobbySnapshot(byte[] data)
    {
        using var stream = new MemoryStream(data);
        var lob = Serializer.Deserialize<CSODOTALobby>(stream);
        var oldLob = _lobby;
        _lobby = lob;
        Client.PostCallback(new PracticeLobbySnapshot(lob, oldLob));
        //UploadRichPresence();
    }

    private void SayHello()
    {
        if (!_isRunning)
            return;
        var clientHello = new ClientGCMsgProtobuf<CMsgClientHello>((uint)EGCBaseClientMsg.k_EMsgGCClientHello);
        clientHello.Body.client_launcher = PartnerAccountType.PARTNER_NONE;
        clientHello.Body.engine = _engine;
        clientHello.Body.secret_key = "";
        clientHello.Body.client_session_need = (uint)EDOTAGCSessionNeed.k_EDOTAGCSessionNeed_UserInUINeverConnected;
        Send(clientHello);
    }

    private void Send(IClientGCMsg msg)
    {
        var clientMsg = new ClientMsgProtobuf<CMsgGCClient>(EMsg.ClientToGC);
        clientMsg.Body.msgtype = MsgUtil.MakeGCMsg(msg.MsgType, msg.IsProto);
        clientMsg.Body.appid = _appId;
        clientMsg.Body.payload = msg.Serialize();
        Client.Send(clientMsg);
    }

    private static IPacketGCMsg GetPacketGCMsg(uint eMsg, byte[] data)
    {
        var realEMsg = MsgUtil.GetGCMsg(eMsg);
        if (MsgUtil.IsProtoBuf(eMsg))
        {
            return new PacketClientGCMsgProtobuf(realEMsg, data);
        }

        return new PacketClientGCMsg(realEMsg, data);
    }
}