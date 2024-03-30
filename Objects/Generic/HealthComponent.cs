using Godot;
using System;

/// <summary>
/// Handles health
/// </summary>
public partial class HealthComponent : ColorRect
{
	[Signal] public delegate void DiedEventHandler();
	
	public float MaxHealth;
	public float Health;

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
		MaxHealth = Health = resource.Health;
		OnHealthChanged();
	}

	/// <summary>
	/// Take some form of damage
	/// </summary>
	/// <param name="amount"></param>
	public void TakeDamage(float amount)
	{
		Health -= amount;

		if (Health < 0)
			Health = 0;

		OnHealthChanged();
	}

	/// <summary>
	/// Heal from damage
	/// </summary>
	/// <param name="amount"></param>
	public void Heal(float amount)
	{
		Health += amount;

		if (Health > MaxHealth)
			Health = MaxHealth;

		OnHealthChanged();
	}

	/// <summary>
	/// Update our health bar and signal that we're dead
	/// </summary>
	private void OnHealthChanged()
	{
        _healthBar.SetSize(new Vector2((Health / MaxHealth) * Size.X, _healthBar.Size.Y));
        if (Health <= 0)
		{
			EmitSignal(SignalName.Died);
		}
	}
}
