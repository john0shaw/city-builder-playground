using Godot;
using System;

[GlobalClass]
public partial class TileData : Resource
{
    [Export] public int SourceID;
    [Export] public Vector2I AtlasCoords;

    public int Layer;
    public Vector2I Position;
    public int AlternativeTile = 0;
}
