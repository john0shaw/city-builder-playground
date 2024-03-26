using Godot;
using Godot.Collections;
using System;

/// <summary>
/// Move to a target position
/// </summary>
public partial class MoveToPosition : StateComponent
{
    public const string POSITION_KEY = "position";

	private Vector2 _target = Vector2.Inf;
    private UnitNode _unitNode;

    /// <summary>
    /// Grab the root unit node
    /// </summary>
    public override void Initialize()
    {
        _unitNode = (UnitNode)StateMachine.RootNode;
    }

    /// <summary>
    /// Set navigation to the target
    /// </summary>
    /// <param name="message"></param>
    public override void Enter(Dictionary message = null)
    {
        if (message.ContainsKey(POSITION_KEY))
        {
            _unitNode.Navigation.MoveTo((Vector2)message[POSITION_KEY]);
            _unitNode.Navigation.NavigationFinished += _on_arrived_at_target;
        }
    }

    /// <summary>
    /// Deregister for arrival notification
    /// </summary>
    public override void Exit()
    {
        _unitNode.Navigation.NavigationFinished -= _on_arrived_at_target;
    }

    /// <summary>
    /// Go back to idling when we've reached our target
    /// </summary>
    public void _on_arrived_at_target()
    {
        StateMachine.ChangeTo("Idle");
    }
}
