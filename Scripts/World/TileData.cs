using Godot;
using System;

[GlobalClass]
public partial class TileData : Resource
{
    [Export] public int SourceID;
    [Export] public Vector2I AtlasCoords;
    [Export] public int AlternativeTile = 0;

    public int Layer;
    public Vector2I Position;
}
