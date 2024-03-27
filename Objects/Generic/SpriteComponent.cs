using Godot;
using System;

/// <summary>
/// Handles multi-color sprites (for different teams)
/// </summary>
public partial class SpriteComponent : Sprite2D
{
    private BuildableObjectResource.ColorEnum _color;

    /// <summary>
    /// Configure the SpriteComponent
    /// </summary>
    /// <param name="resource"></param>
	public void Initialize(BuildableObjectResource resource, BuildableObjectResource.ColorEnum color)
	{
        _color = color;
        SetSprite(resource);
        Hframes = resource.SpriteSheetColumns;
        Vframes = resource.SpriteSheetRows;
	}

    /// <summary>
    /// Set the sprite to the correct color
    /// </summary>
    /// <param name="resource"></param>
    private void SetSprite(BuildableObjectResource resource)
    {
        switch (_color)
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
