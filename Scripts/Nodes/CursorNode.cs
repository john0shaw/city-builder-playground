using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class CursorNode : Node2D
{

	[Export] public GameManagerNode GameManager;
    [Export] public float SingleClickDistance = 5.0f;

    private bool _isDraggingSelection = false;
    private Vector2 _dragSelectionStart;
    private Vector2 _dragSelectionEnd;

    public override void _Process(double delta)
    {

    }

    /// <summary>
    /// Draw a selection rectangle when dragging
    /// </summary>
    public override void _Draw()
    {
        base._Draw();

        if (_isDraggingSelection )
        {
            Vector2 mousePosition = GetGlobalMousePosition();
            DrawRect(
                new Rect2(
                    _dragSelectionStart.X, 
                    _dragSelectionStart.Y, 
                    mousePosition.X - _dragSelectionStart.X, 
                    mousePosition.Y - _dragSelectionStart.Y
                    ), 
                Color.FromHtml("dddddd"), 
                false, 
                1.0f
                );
        }
    }

    /// <summary>
    /// Handle incomming input
    /// </summary>
    /// <param name="event"></param>
    public override void _Input(InputEvent @event)
    {
        HandleSelection(@event);
    }

    /// <summary>
    /// Process events to select objects
    /// </summary>
    /// <param name="event"></param>
    private void HandleSelection(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.ButtonIndex == MouseButton.Left)
        {
            if (mouseEvent.Pressed) 
                StartSelection();
            else 
                EndSelection();
        }
        else
        {
            QueueRedraw();
        }
    }

    /// <summary>
    /// Start tracking for mouse selection
    /// </summary>
    private void StartSelection()
    {
        _dragSelectionStart = GetGlobalMousePosition();
        _isDraggingSelection = true;
    }

    /// <summary>
    /// End tracking for mouse selection
    /// </summary>
    private void EndSelection()
    {
        _dragSelectionEnd = GetGlobalMousePosition();
        _isDraggingSelection = false;
        QueueRedraw();

        GameManager.UnitManager.SelectedUnits = QueryForUnitsInSelection();
    }

    /// <summary>
    /// Query for units in the dragged selection
    /// </summary>
    /// <returns></returns>
    private List<UnitNode> QueryForUnitsInSelection()
    {
        RectangleShape2D selectionRect = new()
        {
            Size = GetSizeFromDragStart(_dragSelectionEnd)
        };

        PhysicsShapeQueryParameters2D query = new()
        {
            Shape = selectionRect,
            CollisionMask = 0b00000000_00000000_00000000_00000010,
            Transform = new Transform2D(0, GetCenterFromDragStart(_dragSelectionEnd))
        };

        List<UnitNode> foundUnits = new();
        foreach (Dictionary found in GetWorld2D().DirectSpaceState.IntersectShape(query))
        {
            if (found["collider"].Obj is UnitNode unitNode && unitNode.Color == GameManager.PlayerColor)
            {
                foundUnits.Add(unitNode);
            }
        }

        return foundUnits;
    }

    /// <summary>
    /// Get the total square size from the drag start position to a given end position
    /// </summary>
    /// <param name="endPosition"></param>
    /// <returns></returns>
    public Vector2 GetSizeFromDragStart(Vector2 endPosition)
    {
        return new Vector2(
                    Mathf.Abs(endPosition.X - _dragSelectionStart.X),
                    Mathf.Abs(endPosition.Y - _dragSelectionStart.Y)
                );
    }

    /// <summary>
    /// Get the center from the drag start position to a given end position
    /// </summary>
    /// <param name="endPosition"></param>
    /// <returns></returns>
    public Vector2 GetCenterFromDragStart(Vector2 endPosition)
    {
        return new Vector2(
                    (endPosition.X + _dragSelectionStart.X) / 2,
                    (endPosition.Y + _dragSelectionStart.Y) / 2
                );
    }
}
