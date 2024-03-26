using Godot;
using Godot.Collections;
using System;

/// <summary>
/// Provide state machine functionality to a node
/// </summary>
[GlobalClass]
public partial class StateMachineComponent : Label
{
    [Signal] public delegate void state_changeEventHandler(string stateName, Dictionary payload = null);

    [Export] public bool Debug = false;
    [Export] protected NodePath InitialState;

    public StateComponent State;
    public Node RootNode;

    /// <summary>
    /// Set up the state machine
    /// </summary>
    public override void _Ready()
    {
        RootNode = GetOwner<Node>();
        State = GetNode<StateComponent>(InitialState);
        Visible = Debug;

        foreach (StateComponent childState in GetChildren())
        {
            childState.StateMachine = this;
            childState.Initialize();
        }
        State.Enter();
        Text = State.Name;
    }

    /// <summary>
    /// Gets the name of the current state
    /// </summary>
    /// <returns></returns>
    public string StateName()
    {
        return State.Name;
    }

    /// <summary>
    /// Call the current states _Process function
    /// </summary>
    /// <param name="delta"></param>
    public override void _Process(double delta)
    {
        State._Process(delta);
    }

    /// <summary>
    /// Call the current states _PhysicsProcess function
    /// </summary>
    /// <param name="delta"></param>
    public override void _PhysicsProcess(double delta)
    {
        State._PhysicsProcess(delta);
    }

    /// <summary>
    /// Transition to a new state
    /// </summary>
    /// <param name="toState"></param>
    /// <param name="message"></param>
    public void ChangeTo(string toState, Dictionary message = null)
    {
        if (!HasNode(toState))
        {
            Log.Error(Name + ": No state matching " + toState);
            return;
        }

        State.Exit();
        State = GetNode<StateComponent>(toState);
        State.Enter(message);
        Text = State.Name;
        Log.Debug($"{RootNode.Name} entered state {toState}");
        EmitSignal(SignalName.state_change, toState, message);
    }
}
