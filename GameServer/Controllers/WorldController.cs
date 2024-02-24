using GameServer.Controllers.Attributes;
using GameServer.Models;
using GameServer.Network;
using GameServer.Network.Messages;
using GameServer.Settings;
using GameServer.Systems.Event;
using Microsoft.Extensions.Options;
using Protocol;

namespace GameServer.Controllers;
internal class WorldController : Controller
{
    public WorldController(PlayerSession session) : base(session)
    {
        // WorldController.
    }

    [GameEvent(GameEventType.EnterGame)]
    public async Task OnEnterGame(CreatureController creatureController, IOptions<GameplayFeatureSettings> gameplayFeatures)
    {
        
        await creatureController.JoinScene(gameplayFeatures.Value.WorldScene);
    }

    [NetEvent(MessageId.EntityOnLandedRequest)]
    public RpcResult OnEntityOnLandedRequest() => Response(MessageId.EntityOnLandedResponse, new EntityOnLandedResponse());

    [NetEvent(MessageId.PlayerMotionRequest)]
    public RpcResult OnPlayerMotionRequest() => Response(MessageId.PlayerMotionResponse, new PlayerMotionResponse());

    [NetEvent(MessageId.EntityLoadCompleteRequest)]
    public RpcResult OnEntityLoadCompleteRequest() => Response(MessageId.EntityLoadCompleteResponse, new EntityLoadCompleteResponse());

    [NetEvent(MessageId.UpdateSceneDateRequest)]
    public RpcResult OnUpdateSceneDateRequest() => Response(MessageId.UpdateSceneDateResponse, new UpdateSceneDateResponse());
}
