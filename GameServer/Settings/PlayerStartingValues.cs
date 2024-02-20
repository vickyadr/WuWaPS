﻿using Protocol;

namespace GameServer.Settings;
internal class PlayerStartingValues
{
    public required string Name { get; set; }
    public required int[] Characters { get; set; }
    public required Vector Position { get; set; }
}
