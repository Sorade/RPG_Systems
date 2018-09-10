using System;

[Serializable]
public enum AttrModType
{
    FLAT = 100,
    PERCENT_MULT = 200,
    PERCENT_ADD = 300,
}
[Serializable]
public enum Attributes
{
    AGILITY,
    DEXTERITY,
    STRENGHT,
    INTELLIGENCE,
    HEALTH,
}
[Serializable]
public enum EffectName
{
    BURN,
    HEAL,
}
[Serializable]
public enum EffectApplicatorType
{
    ENTRY,
    TICK,
    LEAVE
}
[Serializable]
public enum DamageType
{
    FIRE,
    BURN,
    PHYSICAL
}