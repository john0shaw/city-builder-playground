using Godot;
using System;

/// <summary>
/// Handles multi-color sprites (for different teams) for buildings
/// </summary>
public partial class BuildingSpriteComponent : Sprite2D
{
    public bool Constructed
    {
        get => _constructed;
        set
        {
            _constructed = value;
            SetSprite(_constructed, _color);
        }
    }
    private bool _constructed = false;

    public Team.ColorEnum Color
    {
        get => _color;
        set
        {
            _color = value;
            SetSprite(_constructed, _color);
        }
    }
    private Team.ColorEnum _color = Team.ColorEnum.White;

    /// <summary>
    /// Set the sprite to the correct state and color
    /// </summary>
    /// <param name="resource"></param>
    private void SetSprite(bool constructed, Team.ColorEnum color)
    {
        if (!constructed)
        {
            Frame = 0;
            return;
        }

        switch (color)
        {
            case Team.ColorEnum.White:
                Frame = 1;
                break;
            case Team.ColorEnum.Blue:
                Frame = 2;
                break;
            case Team.ColorEnum.Red:
                Frame = 3;
                break;
            case Team.ColorEnum.Green:
                Frame = 4;
                break;
            case Team.ColorEnum.Purple:
                Frame = 5;
                break;
        }
    }
}
