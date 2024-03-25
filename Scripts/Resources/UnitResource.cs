using Godot;
using System;

[GlobalClass]
public partial class UnitResource : BuildableItemResource
{
    public enum TypeEnum
    {
        Worker
    };

    [Export] public TypeEnum Type;
}
