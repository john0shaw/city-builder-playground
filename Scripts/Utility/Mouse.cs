using Godot;
using System;

/// <summary>
/// Some basic mouse helpers
/// </summary>
public static class Mouse
{
    /// <summary>
    /// Is the incomming event a right click?
    /// </summary>
    /// <param name="event"></param>
    /// <returns></returns>
    public static bool IsRightClick(InputEvent @event)
    {
        if (@event is not InputEventMouseButton mbEvent) return false;

        return mbEvent.ButtonIndex == MouseButton.Right && !mbEvent.Pressed;
    }

    /// <summary>
    /// Is the incomming event a left click?
    /// </summary>
    /// <param name="event"></param>
    /// <returns></returns>
    public static bool IsLeftClick(InputEvent @event)
    {
        if (@event is not InputEventMouseButton mbEvent) return false;

        return mbEvent.ButtonIndex == MouseButton.Left && !mbEvent.Pressed;
    }
}
