using Godot;
using System;
using System.Diagnostics.Contracts;

/// <summary>
/// Handles basic unit animation based on velocity and if an action is being done
/// </summary>
public partial class UnitAnimationComponent : AnimationPlayer
{
	[Signal] public delegate void ActionTriggeredEventHandler();

	private UnitNode _unitNode;
	private bool _inActionAnimation = false;

	/// <summary>
	/// Pass the unit to the animation node from the parent
	/// </summary>
	/// <param name="unitNode"></param>
	public void Initialize(UnitNode unitNode)
	{
		_unitNode = unitNode;

		AddAnimationLibrary("Idle", unitNode.Resource.AnimationIdle);
		AddAnimationLibrary("Walk", unitNode.Resource.AnimationWalk);
		AddAnimationLibrary("Action", unitNode.Resource.AnimationAction);
	}

	/// <summary>
	/// Play walking animation if no action and moving
	/// </summary>
	/// <param name="delta"></param>
    public override void _Process(double delta)
    {
		if (_inActionAnimation) return;

		if (_unitNode.Velocity == Vector2.Zero)
		{
			Play("Idle/Down");
		}
		else
		{
			if (Mathf.Abs(_unitNode.Velocity.X) > Mathf.Abs(_unitNode.Velocity.Y))
			{
				if (_unitNode.Velocity.X > 0)
				{
					Play("Walk/Right");
				}
				else
				{
					Play("Walk/Left");
				}
			}
			else
			{
				if (_unitNode.Velocity.Y > 0)
				{
					Play("Walk/Down");
				}
				else
				{
					Play("Walk/Up");
				}
			}
		}
    }

	/// <summary>
	/// Send off the action_trigger signal
	/// </summary>
	public void TriggerAction() => EmitSignal(SignalName.ActionTriggered);

	/// <summary>
	/// Play the action animation and block other animations until complete.  Relevant
	/// action animation is based on position to nodeForAction
	/// </summary>
	/// <param name="nodeForAction"></param>
	public async void PlayActionAnimation(Node2D nodeForAction)
	{
		_inActionAnimation = true;
		Vector2 direction = _unitNode.GlobalPosition.DirectionTo(nodeForAction.GlobalPosition);

		if (direction == Vector2.Up) Play("Action/Up");
		else if (direction == Vector2.Right) Play("Action/Right");
		else if (direction == Vector2.Left) Play("Action/Left");
		else Play("Action/Down");
		
        await ToSignal(this, "animation_finished");
        _inActionAnimation = false;
    }
}
