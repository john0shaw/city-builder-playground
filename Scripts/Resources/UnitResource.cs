using Godot;
using System;

/// <summary>
/// Holds data about a specific resource
/// </summary>
[GlobalClass]
public partial class UnitResource : BuildableObjectResource
{
    public enum CombatTypeEnum
    {
        NoAttack,
        Melee,
        Ranged
    };

    public enum TypeEnum
    {
        Worker,
        Swordsman
    };

    [Export] public TypeEnum Type;

    [ExportGroup("Stats")]
    [Export] public float Health;
    [Export] public float Speed = 10f;

    [ExportGroup("Combat")]
    [Export] public CombatTypeEnum CombatType = CombatTypeEnum.NoAttack;
    [Export] public float CombatRange = 25f;
    [Export] public float CombatDamageMin;
    [Export] public float CombatDamageMax;
    [Export] public PackedScene CombatProjectile;

    [ExportGroup("Animations")]
    [Export] public AnimationLibrary AnimationIdle;
    [Export] public AnimationLibrary AnimationWalk;
    [Export] public AnimationLibrary AnimationAction;
    [Export] public AnimationLibrary AnimationDie;

    /// <summary>
    /// Determine if this unit type is an attacker
    /// </summary>
    /// <returns></returns>
    public bool IsAttacker()
    {
        return CombatType != CombatTypeEnum.NoAttack;
    }
}
