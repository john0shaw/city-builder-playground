using Godot;
using System;
using System.Collections.Generic;

public partial class UnitManagerNode : Node2D
{
    private List<UnitNode> _selectedUnits = new();
    public List<UnitNode> SelectedUnits
    {
        get => _selectedUnits;
        set
        {
            foreach (UnitNode unit in _selectedUnits)
                unit.Selected = false;

            foreach (UnitNode unit in value)
                unit.Selected = true;

            _selectedUnits = value;
        }
    }

    /// <summary>
    /// Command any selected units to move to a position on right click
    /// </summary>
    /// <param name="event"></param>
    public override void _Input(InputEvent @event)
    {
        if (SelectedUnits.Count > 0 && Mouse.IsRightClick(@event))
        {
            foreach (UnitNode unit in SelectedUnits)
            {
                unit.StateMachine.ChangeTo(
                    State.MoveToPosition.StateName, 
                    new Godot.Collections.Dictionary { { 
                            State.MoveToPosition.POSITION_KEY, GetGlobalMousePosition() 
                        } }
                    );
            }
        }
    }
}
