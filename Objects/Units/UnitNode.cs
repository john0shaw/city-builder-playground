using Godot;
using System;
using Godot.Collections;
using System.Runtime.Serialization;

public partial class UnitNode : CharacterBody2D
{
	[Export] public UnitResource Resource;
	[Export] public Team.ColorEnum Color;
	[Export] public bool DebugEnabled;

	public UnitAnimationComponent Animation;
    public SpriteComponent Sprite;
    public StateMachineComponent StateMachine;
    public NavigationComponent Navigation;
	public HealthComponent Health;
	public DetectionComponent Detection;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		InitializeChildren();
		InitializeStates();
		SetDebug(DebugEnabled);
	}

	/// <summary>
	/// Show/Hide debug states
	/// </summary>
	/// <param name="enabled"></param>
	public void SetDebug(bool enabled)
	{
		StateMachine.SetDebug(enabled);
		Navigation.DebugEnabled = enabled;
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
}
