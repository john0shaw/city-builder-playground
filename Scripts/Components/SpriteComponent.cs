using Godot;
using System;

/// <summary>
/// Handles multi-color sprites (for different teams)
/// </summary>
public partial class SpriteComponent : Sprite2D
{
    [Export] public BuildableObjectResource.ColorEnum Color;

    /// <summary>
    /// Configure the SpriteComponent
    /// </summary>
    /// <param name="resource"></param>
	public void Initialize(BuildableObjectResource resource)
	{
        SetSprite(resource);
	}

    /// <summary>
    /// Set the sprite to the correct color
    /// </summary>
    /// <param name="resource"></param>
    private void SetSprite(BuildableObjectResource resource)
    {
        switch (Color)
        {
            case BuildableObjectResource.ColorEnum.White:
                Texture = resource.SpriteSheetWhite;
                break;
            case BuildableObjectResource.ColorEnum.Purple:
                Texture = resource.SpriteSheetPurple;
                break;
            case BuildableObjectResource.ColorEnum.Blue:
                Texture = resource.SpriteSheetBlue;
                break;
            case BuildableObjectResource.ColorEnum.Green:
                Texture = resource.SpriteSheetGreen;
                break;
            case BuildableObjectResource.ColorEnum.Red:
                Texture = resource.SpriteSheetRed;
                break;
        }
    }
}
