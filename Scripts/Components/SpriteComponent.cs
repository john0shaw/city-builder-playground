using Godot;
using System;

/// <summary>
/// Handles multi-color sprites (for different teams)
/// </summary>
public partial class SpriteComponent : Sprite2D
{
    [Export] public BuildableItemResource.ColorEnum Color;

    /// <summary>
    /// Configure the SpriteComponent
    /// </summary>
    /// <param name="resource"></param>
	public void Configure(BuildableItemResource resource)
	{
        SetSprite(resource);
	}

    /// <summary>
    /// Set the sprite to the correct color
    /// </summary>
    /// <param name="resource"></param>
    private void SetSprite(BuildableItemResource resource)
    {
        switch (Color)
        {
            case BuildableItemResource.ColorEnum.White:
                Texture = resource.SpriteSheetWhite;
                break;
            case BuildableItemResource.ColorEnum.Purple:
                Texture = resource.SpriteSheetPurple;
                break;
            case BuildableItemResource.ColorEnum.Blue:
                Texture = resource.SpriteSheetBlue;
                break;
            case BuildableItemResource.ColorEnum.Green:
                Texture = resource.SpriteSheetGreen;
                break;
            case BuildableItemResource.ColorEnum.Red:
                Texture = resource.SpriteSheetRed;
                break;
        }
    }
}
