using Godot;
using System;
using Godot.Collections;

public partial class UnitNode : CharacterBody2D
{
	[Export] public UnitResource Resource;
	[Export] public BuildableObjectResource.ColorEnum Color;
	[Export] public bool DebugEnabled;

	public UnitAnimationComponent Animation;
    public SpriteComponent Sprite;
    public StateMachineComponent StateMachine;
    public NavigationComponent Navigation;
	public HealthComponent Health;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();

		Animation = GetNode<UnitAnimationComponent>("AnimationComponent");
		StateMachine = GetNode<StateMachineComponent>("StateMachineComponent");
		Navigation = GetNode<NavigationComponent>("NavigationComponent");
		Sprite = GetNode<SpriteComponent>("SpriteComponent");
		Health = GetNode<HealthComponent>("HealthComponent");

        Animation.Initialize(this);
		Navigation.Initialize(this);
        Sprite.Initialize(Resource, Color);
		Health.Initialize(Resource);

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

    public override void _Input(InputEvent @event)
    {
        if (Mouse.IsRightClick(@event))
		{
			StateMachine.ChangeTo("MoveToPosition", new Dictionary { { MoveToPosition.POSITION_KEY, GetGlobalMousePosition() }  } );
		}
    }
}
