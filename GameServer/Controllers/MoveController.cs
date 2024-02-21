using System.Numerics;
using GameServer.Controllers.Attributes;
using GameServer.Models;
using GameServer.Network;
using Protocol;

namespace GameServer.Controllers;

internal class MoveController : Controller
{
    private readonly ModelManager _modelManager;
    private readonly PlayerSession _session;
    
    public MoveController(PlayerSession session, ModelManager modelManager) : base(session)
    {
        _modelManager = modelManager;
        _session = session;
    }

    [NetEvent(MessageId.MovePackagePush)]
    public void OnMovePackagePush(MovePackagePush movePackagePush)
    {
        _modelManager.Player.Position.MergeFrom(movePackagePush.MovingEntities[0].MoveInfos[0].Location);
    }
}