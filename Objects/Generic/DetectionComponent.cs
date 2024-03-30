using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// Handles detection of objects (enemies or resources)
/// </summary>
public partial class DetectionComponent : Area2D
{
	[Export] public float Range = 100f;

	public Array<UnitNode> Enemies = new();
	public Array<Node> Resources = new();

	private List<Team.ColorEnum> _enemyFilter = new();
	private CollisionShape2D _collisionShape;

	/// <summary>
	/// Sets up detection range and configures object
	/// </summary>
	public override void _Ready()
	{
		_collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
		_collisionShape.Shape = new CircleShape2D() { Radius = Range };
    }

	/// <summary>
	/// Sets up the colors that would be considered enemies
	/// </summary>
	/// <param name="unitNode"></param>
    public void SetEnemyFilter(List<Team.ColorEnum> enemyColors)
	{
		_enemyFilter = enemyColors;

		// Remove any enemies that we're actively tracking
		foreach (UnitNode enemy in Enemies)
		{
			if (!enemyColors.Contains(enemy.Color))
				Enemies.Remove(enemy);
		}
	}

	/// <summary>
	/// Get the closest nearby enemy
	/// </summary>
	/// <returns></returns>
	public UnitNode GetClosestEnemy()
	{
		if (Enemies.Count == 0)
			return null;

		UnitNode closestUnit = Enemies[0];
		float closestUnitDistance = float.PositiveInfinity;
		foreach (UnitNode unitNode in Enemies)
		{
			float distanceToUnit = GlobalPosition.DistanceTo(unitNode.GlobalPosition);
            if (distanceToUnit < closestUnitDistance)
			{
				closestUnitDistance = distanceToUnit;
				closestUnit = unitNode;
			}
		}

		return closestUnit;
	}

	/// <summary>
	/// Adds objects to tracking if they enter the detection area.
	/// Only adds to Enemies if the unit is of a different color
	/// </summary>
	/// <param name="node"></param>
    public void _on_body_entered(Node2D node)
	{
		if (node is UnitNode unitNode && _enemyFilter.Contains(unitNode.Color) && !Enemies.Contains(unitNode))
		{
			Enemies.Add(unitNode);
		}
	}

	/// <summary>
	/// Removes objects from tracking if they leave the detection area
	/// </summary>
	/// <param name="node"></param>
	public void _on_body_exited(Node2D node)
	{
		if (node is UnitNode unitNode && Enemies.Contains(unitNode))
		{
			Enemies.Remove(unitNode);
		}
	}
}
