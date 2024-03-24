using Godot;
using System;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// Handles logging to GD Console, UI and file.  GD Console/UI are in a rotating
/// list, but file logging will always include all levels
/// </summary>
public static class Log
{
    public enum LevelEnum
    {
        Debug,
        Info,
        Error
    };

    public const int MAX_UI_ROWS = 50;
    public const string LOG_FOLDER = "user://Logs";

    public static LevelEnum Level = LevelEnum.Info;
    public static List<string> Messages = new List<string>();

    private static string _logFile = null;

    /// <summary>
    /// Write a debug message
    /// </summary>
    /// <param name="message"></param>
    public static void Debug(string message)=> AddMessage(LevelEnum.Debug, $"DBG : {message}"); 

    /// <summary>
    /// Write an info message
    /// </summary>
    /// <param name="message"></param>
    public static void Info(string message) => AddMessage(LevelEnum.Info, $"INFO: {message}");
    
    /// <summary>
    /// Write an error message
    /// </summary>
    /// <param name="message"></param>
    public static void Error(string message) => AddMessage(LevelEnum.Error, $"ERR : {message}");

    /// <summary>
    /// Writes the message to file and GD Console.  Add to UI log if level enabled.
    /// </summary>
    /// <param name="level"></param>
    /// <param name="message"></param>
    private static void AddMessage(LevelEnum level, string message)
    {
        WriteMessageToFile(message);
        GD.Print(message);
        if (Level >= level)
        {
            AppendMessageToList(message);
        }
    }

    /// <summary>
    /// Add a message to the UI
    /// </summary>
    /// <param name="message"></param>
    private static void AppendMessageToList(string message)
    {
        Messages.Add(message);
        if (Messages.Count > MAX_UI_ROWS)
        {
            Messages.Remove(Messages[0]);
        }
    }

    /// <summary>
    /// Adds a message to the file log
    /// </summary>
    /// <param name="message"></param>
    private static void WriteMessageToFile(string message)
    {
        using (StreamWriter streamWriter = File.AppendText(GetLogFile()))
        {
            streamWriter.WriteLine(message);
        }
    }

    /// <summary>
    /// Gets the log file name - on first call, will create a new file with a timestamp of the first logged message
    /// </summary>
    /// <returns></returns>
    private static string GetLogFile()
    {
        if (_logFile == null)
        {
            _logFile = Path.Combine(
                ProjectSettings.GlobalizePath(LOG_FOLDER),
                DateTime.Now.ToString("yyyy-MM-dd HH-mm") + ".log"
                );
        }

        return _logFile;
    }
}
