using Godot;
using System;

/// <summary>
/// Basic overview camera for moving around
/// </summary>
public partial class OverviewCameraNode : CharacterBody2D
{
    [Export] public float Speed = 300.0f;

    /// <summary>
    /// Provide basic camera movement based on speed
    /// </summary>
    /// <param name="delta"></param>
    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
        Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");
        if (direction != Vector2.Zero)
        {
            velocity = direction * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}
