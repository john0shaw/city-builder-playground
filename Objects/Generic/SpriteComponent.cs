using Godot;
using System;

/// <summary>
/// Handles multi-color sprites (for different teams)
/// </summary>
public partial class SpriteComponent : Sprite2D
{
    private Team.ColorEnum _color;

    /// <summary>
    /// Configure the SpriteComponent
    /// </summary>
    /// <param name="resource"></param>
	public void Initialize(BuildableObjectResource resource, Team.ColorEnum color)
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
            case Team.ColorEnum.White:
                Texture = resource.SpriteSheetWhite;
                break;
            case Team.ColorEnum.Purple:
                Texture = resource.SpriteSheetPurple;
                break;
            case Team.ColorEnum.Blue:
                Texture = resource.SpriteSheetBlue;
                break;
            case Team.ColorEnum.Green:
                Texture = resource.SpriteSheetGreen;
                break;
            case Team.ColorEnum.Red:
                Texture = resource.SpriteSheetRed;
                break;
        }
    }
}
