using Godot;
using System;
using System.Xml.XPath;

[GlobalClass]
public partial class ItemResource : Resource
{
    [Export] public int Id;
    [Export] public string Name;
    [Export] public Texture2D Icon;
}