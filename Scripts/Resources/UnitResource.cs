using Godot;
using System;

[GlobalClass]
public partial class UnitResource : BuildableObjectResource
{
    public enum TypeEnum
    {
        Worker
    };

    [Export] public TypeEnum Type;
}
