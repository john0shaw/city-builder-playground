using Godot;
using System;

[GlobalClass]
public partial class ItemCountResource : Resource
{
    [Export] public ItemResource Item;
    [Export] public int Count;
}