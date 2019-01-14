using System;

[Serializable, Flags]
public enum TargetType
{
    MONSTER = 1 << 1,
    PLAYER = 1 << 2,
    SELF = 1 << 3
}

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
    DMG_PHYS,
    DMG_FIRE,
    DMG_COLD,
    DMG_POISON

}

[Serializable]
public enum TargetingType
{
    UNIQUE,
    ALL,
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
    PHYSICAL,
    COLD,
    POISON
}