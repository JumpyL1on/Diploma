using SteamKit2;
using SteamKit2.GC;
using SteamKit2.GC.Dota.Internal;
using SteamKit2.Internal;

namespace Diploma.WebAPI.BusinessLogic;

public class SteamBot : IDisposable
{
    private readonly SteamClient _client;
    private readonly SteamUser _user;
    private readonly SteamGameCoordinator _gameCoordinator;
    private readonly CallbackManager _callbackManager;
    private bool _isLaunched;
    private bool _isBusy;
    private const uint AppId = 570;

    public SteamBot(string userName, string password)
    {
        _client = new SteamClient();

        _user = _client.GetHandler<SteamUser>()!;

        _gameCoordinator = _client.GetHandler<SteamGameCoordinator>()!;

        _callbackManager = new CallbackManager(_client);

        _callbackManager.Subscribe<SteamClient.ConnectedCallback>(OnConnected);

        _callbackManager.Subscribe<SteamUser.LoggedOnCallback>(OnLoggedOn);

        _callbackManager.Subscribe<SteamGameCoordinator.MessageCallback>(OnGCMessage);

        _isLaunched = false;

        _isBusy = false;
    }


    public void Launch()
    {
        Console.WriteLine("Connecting to Steam...");

        _client.Connect();

        while (!_isLaunched)
        {
            _callbackManager.RunWaitCallbacks(TimeSpan.FromSeconds(1));
        }
    }

    public void ProcessMatch()
    {
        _isBusy = true;

        var message = new ClientGCMsgProtobuf<CMsgPracticeLobbyCreate>((uint)EDOTAGCMsg.k_EMsgGCPracticeLobbyCreate);
        
        _gameCoordinator.Send(message, AppId);
        
        while (_isBusy)
        {
            _callbackManager.RunWaitCallbacks(TimeSpan.FromSeconds(1));
        }
    }

    public void Dispose() => _user.LogOff();

    private void OnConnected(SteamClient.ConnectedCallback callback)
    {
        Console.WriteLine("Connected to Steam! Logging in user");

        _user.LogOn(new SteamUser.LogOnDetails
        {
            Username = "username",
            Password = "password",
        });
    }

    private void OnLoggedOn(SteamUser.LoggedOnCallback callback)
    {
        if (callback.Result != EResult.OK)
        {
            Console.WriteLine("Unable to logon to Steam: {0} / {1}", callback.Result, callback.ExtendedResult);
            return;
        }

        Console.WriteLine("Successfully logged on!");

        var playGame = new ClientMsgProtobuf<CMsgClientGamesPlayed>(EMsg.ClientGamesPlayed);

        playGame.Body.games_played.Add(new CMsgClientGamesPlayed.GamePlayed
        {
            game_id = new GameID(AppId),
        });

        _client.Send(playGame);

        Thread.Sleep(5000);

        var clientHello = new ClientGCMsgProtobuf<CMsgClientHello>((uint)EGCBaseClientMsg.k_EMsgGCClientHello);

        clientHello.Body.engine = ESourceEngine.k_ESE_Source2;

        _gameCoordinator.Send(clientHello, AppId);
    }

    private void OnGCMessage(SteamGameCoordinator.MessageCallback callback)
    {
        var messageMap = new Dictionary<uint, Action<IPacketGCMsg>>
        {
            { (uint)EGCBaseClientMsg.k_EMsgGCClientWelcome, OnClientWelcome },
            { (uint)EDOTAGCMsg.k_EMsgGCMatchDetailsResponse, OnMatchDetails },
            { (uint) EDOTAGCMsg.k_EMsgGCToClientMatchSignedOut, OnMatchSignedOut}
        };

        if (!messageMap.TryGetValue(callback.EMsg, out var func))
        {
            return;
        }

        func(callback.Message);
    }

    private void OnClientWelcome(IPacketGCMsg packetMsg)
    {
        var msg = new ClientGCMsgProtobuf<CMsgClientWelcome>(packetMsg);

        Console.WriteLine("GC is welcoming us. Version: {0}", msg.Body.version);

        _isLaunched = true;
    }

    private void OnMatchDetails(IPacketGCMsg packetMsg)
    {
        var msg = new ClientGCMsgProtobuf<CMsgGCMatchDetailsResponse>(packetMsg);

        var result = (EResult)msg.Body.result;
        if (result != EResult.OK)
        {
            Console.WriteLine("Unable to request match details: {0}", result);
        }
    }

    private void OnMatchSignedOut(IPacketGCMsg packetMsg)
    {
        var msg = new ClientGCMsgProtobuf<CMsgGCToClientMatchSignedOut>(packetMsg);
    }
}