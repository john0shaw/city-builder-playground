using Godot;
using System;
using Godot.Collections;
using System.Runtime.Serialization;

public partial class UnitNode : CharacterBody2D
{
	[Export] public UnitResource Resource;
	[Export] public Team.ColorEnum Color;

	
	public bool Selected
	{
		get => _selected;
		set => SelectedIndicator.Visible = _selected = value;
	}
    private bool _selected;

    public UnitAnimationComponent Animation;
    public SpriteComponent Sprite;
    public StateMachineComponent StateMachine;
    public NavigationComponent Navigation;
	public HealthComponent Health;
	public DetectionComponent Detection;
	public Sprite2D SelectedIndicator;

	/// <summary>
	/// Setup the node references and initial state
	/// </summary>
	public override void _Ready()
	{
		InitializeChildren();
		InitializeStates();
	}

	/// <summary>
	/// Take damage from some source
	/// </summary>
	/// <param name="damage"></param>
	public void TakeDamage(float damage)
	{
		if (Health.Current > 0)
			Health?.TakeDamage(damage);
	}

	/// <summary>
	/// Set state-specific options based on UnitResource
	/// </summary>
	private void InitializeStates()
	{
		State.Idle idle = GetNode<State.Idle>("StateMachineComponent/Idle");
		State.Attack attack = GetNode<State.Attack>("StateMachineComponent/Attack");

		idle.IsAttacker = Resource.IsAttacker();
		attack.DamageMin = Resource.CombatDamageMin;
		attack.DamageMax = Resource.CombatDamageMax;
	}

	/// <summary>
	/// Get and initialize all children components
	/// </summary>
	private void InitializeChildren()
	{
        Animation = GetNode<UnitAnimationComponent>("AnimationComponent");
        StateMachine = GetNode<StateMachineComponent>("StateMachineComponent");
        Navigation = GetNode<NavigationComponent>("NavigationComponent");
        Sprite = GetNode<SpriteComponent>("SpriteComponent");
        Health = GetNode<HealthComponent>("HealthComponent");
        Detection = GetNode<DetectionComponent>("DetectionComponent");
		SelectedIndicator = GetNode<Sprite2D>("SelectedIndicator");

        Animation.Initialize(this);
        Navigation.Initialize(this);
        Sprite.Initialize(Resource, Color);
        Health.Initialize(Resource);
        Detection.SetEnemyFilter(Team.GetOtherTeamColors(Color));
    }

	/// <summary>
	/// If we've died, clean up and remove
	/// </summary>
	public void _on_health_component_died()
	{
		Log.Info($"{Name} has died");
		QueueFree();
	}
}
