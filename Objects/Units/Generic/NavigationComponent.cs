using Godot;
using System;

/// <summary>
/// Handles movement for a CharacterBody2D that will move around the map
/// </summary>
[GlobalClass]
public partial class NavigationComponent : NavigationAgent2D
{
    [Signal] public delegate void arrived_at_targetEventHandler();

    [Export] public float TargetDeadband = 1f;

    float Speed = 10f;
    public bool Moving;
    public UnitNode UnitNode;

    public Vector2 DirectTargetPosition = Vector2.Inf;

    private float _movementDeadband = 1f;

    public override void _Ready()
    {
        VelocityComputed += _on_velocity_computed;
    }

    public void Initialize(UnitNode unit)
    {
        UnitNode = unit;
        Speed = unit.Resource.Speed;
    }

    /// <summary>
    /// Move towards a position and check if the unit is close
    /// </summary>
    /// <param name="delta"></param>
    public override void _PhysicsProcess(double delta)
    {
        if (IsNavigationFinished()) return;

        Vector2 nextPathPosition = GetNextPathPosition();
        Vector2 newVelocity = (nextPathPosition - UnitNode.GlobalPosition).Normalized() * Speed;

        if (AvoidanceEnabled)
            Velocity = newVelocity;
        else
            _on_velocity_computed(newVelocity);
    }

    private void _on_velocity_computed(Vector2 safeVelocity)
    {
        UnitNode.Velocity = safeVelocity;
        UnitNode.MoveAndSlide();
    }

    /// <summary>
    /// Stop all movement
    /// </summary>
    public void Stop()
    {
        Moving = false;
        DirectTargetPosition = Vector2.Inf;
        UnitNode.Velocity = Vector2.Zero;
    }

    /// <summary>
    /// Set the target position to begin moving.  Attempt to get within a tight deadband.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="deadband"></param>
    public void MoveTo(Vector2 target)
    {
        _movementDeadband = TargetDeadband;
        DirectTargetPosition = target;
        TargetPosition = target;
        Moving = true;
    }
}
