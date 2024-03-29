using System;
using System.Collections.Generic;
using System.Linq;

public class Team
{
    public enum ColorEnum
    {
        White,
        Blue,
        Red,
        Green,
        Purple
    }

    /// <summary>
    /// Get all colors which are NOT the parameter color
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static List<ColorEnum> GetOtherTeamColors(ColorEnum color)
    {
        List<ColorEnum> colors = new();

        foreach (ColorEnum checkColor in Enum.GetValues(typeof(ColorEnum)).Cast<ColorEnum>())
        {
            if (color != checkColor)
                colors.Add(checkColor);
        }

        return colors;
    }
}
