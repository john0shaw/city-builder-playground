using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class WorldNode : Node
{
    private WorldTileMapNode _tileMap;

    public override void _Ready()
    {
        _tileMap = GetNode<WorldTileMapNode>("TileMap");

        _tileMap.RandomizeGrass();
    }
}
