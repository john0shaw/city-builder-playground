using Godot;
using System;

/// <summary>
/// Handles health
/// </summary>
public partial class HealthComponent : ColorRect
{
	[Signal] public delegate void DiedEventHandler();
	[Signal] public delegate void HealthChangedEventHandler();
	
	public float Max;
	public float Current;

	private ColorRect _healthBar;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_healthBar = GetNode<ColorRect>("ColorRect");
	}

	/// <summary>
	/// Set up the health bar for the specific unit
	/// </summary>
	/// <param name="resource"></param>
	public void Initialize(UnitResource resource)
	{
		Max = Current = resource.Health;
		OnHealthChanged();
	}

	/// <summary>
	/// Take some form of damage
	/// </summary>
	/// <param name="amount"></param>
	public void TakeDamage(float amount)
	{
		Current -= amount;

		if (Current < 0)
			Current = 0;

		OnHealthChanged();
	}

	/// <summary>
	/// Heal from damage
	/// </summary>
	/// <param name="amount"></param>
	public void Heal(float amount)
	{
		Current += amount;

		if (Current > Max)
			Current = Max;

		OnHealthChanged();
	}

	/// <summary>
	/// Update our health bar and signal that we're dead
	/// </summary>
	private void OnHealthChanged()
	{
        if (Current <= 0)
		{
			EmitSignal(SignalName.Died);
		}
		else
		{
            _healthBar.SetSize(new Vector2((Current / Max) * Size.X, _healthBar.Size.Y));
        }

		EmitSignal(SignalName.HealthChanged);
	}
}
