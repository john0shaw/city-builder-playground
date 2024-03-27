using Godot;
using System;

/// <summary>
/// Holds data about a specific resource
/// </summary>
[GlobalClass]
public partial class UnitResource : BuildableObjectResource
{
    public enum TypeEnum
    {
        Worker,
        Swordsman
    };

    [Export] public TypeEnum Type;
    [Export] public float Health;
}
