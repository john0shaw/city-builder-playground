using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Random Number Generation Helpers
/// </summary>
public static class RNG
{
    private static RandomNumberGenerator _generator = new RandomNumberGenerator();

    /// <summary>
    /// Set the seed and randomize
    /// </summary>
    /// <param name="seed"></param>
    public static void Randomize(ulong seed)
    {
        _generator.Seed = seed;
        _generator.Randomize();
    }

    /// <summary>
    /// Return a random floating point between from and to
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static float RandF(float from, float to)
    {
        return _generator.RandfRange(from, to);
    }

    /// <summary>
    /// Return a random integer between from and to
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static int RandI(int from, int to)
    {
        return _generator.RandiRange(from, to);
    }

}