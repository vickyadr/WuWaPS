using Core.Config;
using GameServer.Controllers.Attributes;
using GameServer.Models;
using GameServer.Models.Chat;
using GameServer.Network;
using GameServer.Systems.Entity;
using Protocol;

namespace GameServer.Controllers.ChatCommands;

[ChatCommandCategory("spawn")]
internal class ChatSpawnCommandHandler
{
    private readonly ChatRoom _helperRoom;
    private readonly EntitySystem _entitySystem;
    private readonly EntityFactory _entityFactory;
    private readonly PlayerSession _session;
    private readonly ConfigManager _configManager;
    private readonly CreatureController _creatureController;
    private readonly ModelManager _modelManager;

    public ChatSpawnCommandHandler(ModelManager modelManager, EntitySystem entitySystem, EntityFactory entityFactory, PlayerSession session, ConfigManager configManager, CreatureController creatureController)
    {
        _helperRoom = modelManager.Chat.GetChatRoom(1338);
        _entitySystem = entitySystem;
        _entityFactory = entityFactory;
        _session = session;
        _configManager = configManager;
        _creatureController = creatureController;
        _modelManager = modelManager;
    }

    [ChatCommand("monster")]
    [ChatCommandDesc("/spawn monster [id] [optional : [x] [y] [z]] - spawns monster with specified id")]
    public async Task OnSpawnMonsterCommand(string[] args)
    {
        if (args.Length == 1)
            args = [.. args,
            ((float)_modelManager.Player.Position.X + 250).ToString(),
            ((float)_modelManager.Player.Position.Y + 250).ToString(),
            ((float)_modelManager.Player.Position.Z + 250).ToString()];
        else if (args.Length == 4)
            for (int i = 1; i < 4; i++) args[i] += "00";

        if (args.Length != 4 ||
            !(int.TryParse(args[0], out int levelEntityId) &&
            float.TryParse(args[1], out float x) &&
            float.TryParse(args[2], out float y) &&
            float.TryParse(args[3], out float z)))
        {
            _helperRoom.AddMessage(1338, 0, "Usage: \r\n/spawn monster [id] \r\n/spawn monster [id] [x] [y] [z]");
            return;
        }

        MonsterEntity monster = _entityFactory.CreateMonster(levelEntityId);
        monster.Pos = new()
        {
            X = x,
            Y = y,
            Z = z
        };

        _entitySystem.Create(monster);
        monster.InitProps(_configManager.GetConfig<BasePropertyConfig>(600000100)!); // TODO: monster property config

        await _session.Push(MessageId.EntityAddNotify, new EntityAddNotify
        {
            IsAdd = true,
            EntityPbs = { monster.Pb }
        });

        await _creatureController.UpdateAiHate();

        _helperRoom.AddMessage(1338, 0, $"Successfully spawned monster with id {levelEntityId} at ({x / 100}, {y / 100}, {z / 100})");
    }
}
