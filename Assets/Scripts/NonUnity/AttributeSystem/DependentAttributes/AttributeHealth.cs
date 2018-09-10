using System;
using UnityEngine;

[Serializable]
public class AttributeHealth : Attribute {
    Attribute strength;
    float maxValue;

    public AttributeHealth(Attribute parentStrength, float initialValue) : base()
    {
        strength = parentStrength;
        name = Attributes.HEALTH;
        baseValue = initialValue + strength.value * 3;
    }

    //protected override float CalculateFinalValue()
    //{
    //    float value = base.CalculateFinalValue();
    //    if (value > baseValue)
    //    {
    //        return baseValue;
    //    }
    //    return value;
    //}

     public override void AddModifier(AttrModifier mod)
    {
        float delta = 0f;

        switch (mod.type)
        {
            case AttrModType.FLAT:
                delta = value + mod.baseValue - baseValue;
                break;
            case AttrModType.PERCENT_MULT:
                delta = value * (1f + mod.baseValue) - baseValue;
                break;
            case AttrModType.PERCENT_ADD:
                delta = value * (1f + mod.baseValue) - baseValue;
                break;
        }

        if (delta <= 0) // If baseValue not reached after modifier addition
        {
            base.AddModifier(mod);
        }
        else if (delta > 0) // If the baseValue will be exceeded, then add difference between current value and basevalue
        {
            base.AddModifier(new AttrModifier(baseValue - value, AttrModType.FLAT));
        }
    }
}
