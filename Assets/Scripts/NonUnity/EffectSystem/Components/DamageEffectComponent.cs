using System;
using UnityEngine;
using Sirenix.OdinInspector;


[CreateAssetMenu(menuName = "Effect/Components/Damage")]
public class DamageEffectComponent : EffectComponent
{
    [InfoBox("The damage values are to be positive. Negative damage will not work", "IsValueNegative")]
    [SerializeField]
    private DamageType type;
    [SerializeField]
    private AttrModType modType;

    // overrides the component apply method to handle
    // custom behaviour
    public override void Apply(EffectManager target)
    {
        if (!IsValueNegative())
        {
            parentEffect.SetInputsFromSource();
            target.stats.TakeDamage(Value, type, modType); //the stats class of the character will handle all the modifiers, via damage calculator class
        }
        else
        {
            Debug.LogWarning("Some negative damaged has been passed on in "+this.name+".");
        }
    }

    bool IsValueNegative()
    {
        return Value < 0;
    }
}
