using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Effect/Components/Heal")]
public class HealEffectComponent : EffectComponent {
    [InfoBox("The healing values are to be positive. Negative ones will not work", "IsValueNegative")]
    [SerializeField]
    private AttrModType type;


    // overrides the component apply method to handle
    // custom behaviour
    public override void Apply(EffectManager target)
    {
        if (!IsValueNegative())
        {
            target.stats.Heal(Value, type); //the stats class of the character will handle all the modifiers, via damage calculator class
        }
        else
        {
            Debug.LogWarning("Some negative healing has been passed on in " + this.name + ".");
        }
    }

    bool IsValueNegative()
    {
        return Value < 0;
    }
}
