using Godot;
using System;
using Godot.Collections;
using System.Runtime.Serialization;

public partial class UnitNode : CharacterBody2D
{
	[Export] public UnitResource Resource;
	[Export] public Team.ColorEnum Color;

	public UnitAnimationComponent Animation;
    public SpriteComponent Sprite;
    public StateMachineComponent StateMachine;
    public NavigationComponent Navigation;
	public HealthComponent Health;
	public DetectionComponent Detection;

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
		Health.TakeDamage(damage);
	}

	/// <summary>
	/// TESTING ONLY
	/// </summary>
	/// <param name="event"></param>
    public override void _Input(InputEvent @event)
    {
        if (Mouse.IsRightClick(@event) && Color == GameManagerNode.Instance.PlayerColor)
		{
			StateMachine.ChangeTo(State.MoveToPosition.StateName, new Dictionary { { State.MoveToPosition.POSITION_KEY, GetGlobalMousePosition() }  } );
		}
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
