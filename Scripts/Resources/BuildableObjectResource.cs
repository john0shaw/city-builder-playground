using Godot;
using System;

public partial class BuildableObjectResource : Resource
{
    public enum ColorEnum
    {
        White,
        Blue,
        Red,
        Green,
        Purple
    };

    [Export] public string Name;

    [ExportGroup("SpriteSheets")]
    [Export] public Texture2D SpriteSheetWhite;
    [Export] public Texture2D SpriteSheetBlue;
    [Export] public Texture2D SpriteSheetRed;
    [Export] public Texture2D SpriteSheetGreen;
    [Export] public Texture2D SpriteSheetPurple;
}
