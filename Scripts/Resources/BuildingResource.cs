using Godot;
using Godot.Collections;
using System;

/// <summary>
/// Holds data about a specific building
/// </summary>
[GlobalClass]
public partial class BuildingResource : BuildableObjectResource
{
    [Export] public string Name;
    [Export] public string Description;

    [ExportGroup("Stats")]
    [Export] public float Health;
    [ExportSubgroup("Combat")]
    [Export] public float CombatRange = 25f;

    [ExportGroup("Building")]
    [Export] public Array<ResourceResource> AcceptsResources;
    [Export] public Array<UnitResource> BuildableUnits;

    [ExportGroup("Sprites")]
    [Export] public Texture2D SpriteSheet;
}
