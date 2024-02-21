using System.Linq;
using Core.Config;
using GameServer.Controllers.Attributes;
using GameServer.Models;
using GameServer.Models.Chat;
using GameServer.Network;
using GameServer.Systems.Entity;
using Protocol;

namespace GameServer.Controllers.ChatCommands;

[ChatCommandCategory("tp")]
internal class ChatTeleCommandHandler
{
    private readonly ChatRoom _helperRoom;
    private readonly ModelManager _modelManager;
    private readonly EntitySystem _entitySystem;
    private readonly EntityFactory _entityFactory;
    private readonly PlayerSession _session;
    private readonly ConfigManager _configManager;

    public ChatTeleCommandHandler(ModelManager modelManager, EntitySystem entitySystem, EntityFactory entityFactory, PlayerSession session, ConfigManager configManager)
    {
        _helperRoom = modelManager.Chat.GetChatRoom(1338);
        _modelManager = modelManager;
        _entitySystem = entitySystem;
        _entityFactory = entityFactory;
        _session = session;
        _configManager = configManager;
    }
    
    [ChatCommand("pos")]
    [ChatCommandDesc("/tp pos [optional-MapId] [x] [y] [z] - teleport to specified coordinates")]
    public async Task OnPossitionTeleCommand(string[] args)
    {
        if (args.Length == 3)
            args = args.Prepend("8").ToArray();

        if (args.Length != 4 ||
        !(int.TryParse(args[0], out int mId) &&
        int.TryParse(args[1], out int x) &&
        int.TryParse(args[2], out int y) &&
        int.TryParse(args[3], out int z)))
        {
            _helperRoom.AddMessage(1338, 0, "Usage: \r\n/tp pos [x] [y] [z] \r\n/tp pos [map-id] [x] [y] [z]");
            return;
        }

        await _session.Push(MessageId.TeleportNotify, new TeleportNotify
        {
            PosX = x,
            PosY = y,
            PosZ = z,
            PosA = 0,
            MapId = mId,
            Reason = (int)TeleportReason.Gm, TransitionOption = new TransitionOptionPb
            {
                TransitionType = (int)TransitionType.Empty
            }
        });

        _helperRoom.AddMessage(1338, 0, $"Successfully teleport to ({x}, {y}, {z}){(mId!=8 ? $" map id {mId}":"")}");
    }
}