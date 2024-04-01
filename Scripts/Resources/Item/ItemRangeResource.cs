using Godot;
using System;

[GlobalClass]
public partial class ItemRangeResource : Resource
{
    [Export] public ItemResource Item;
    [Export] public int MinValue;
    [Export] public int MaxValue;

    /// <summary>
    /// Get a random item amount between the Minimum and Maximum
    /// </summary>
    /// <returns></returns>
    public int GetRandom() => RNG.RandI(MinValue, MaxValue);
}