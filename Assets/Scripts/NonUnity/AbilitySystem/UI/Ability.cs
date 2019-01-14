using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

[CreateAssetMenu()]
public class Ability : ScriptableObject, IEffectSource {

    public Sprite icon;
    public bool unlocked;
    public float manaCost;
    public float cooldown; // the cooldown time
    //[ListDrawerSettings(HideAddButton = true)]
    public Effect[] selfEffects; // effects that will be applied to the character using the ability
    //[ListDrawerSettings(HideAddButton = true)]
    public Effect[] targetEffects; // effects that will be applied to the targets using the ability
    public TargetType targetTypes; 

    public AbilityTriggerable trigger;

    public void ApplyOnSelf(EffectManager self)
    {
        for (int i = 0; i < selfEffects.Length; i++)
        {
            if (self != null)
            {
                self.AddEffect(selfEffects[i].CloneInitializedToSource(this));
            }
        }
    }

    public void ApplyOnTarget(EffectManager target)
    {
        for (int i = 0; i < targetEffects.Length; i++)
        {
            if (target != null)
            {
                Debug.Log("Applying " + targetEffects[i].name + " on " + name);
                target.AddEffect(targetEffects[i].CloneInitializedToSource(this));
            }
        }
    }

    public float GetEffectInputValue(Attributes attribute)
    {
        return trigger.character.stats.attributes[attribute].value;
    }

    public void RemoveEffects()
    {
        throw new System.NotImplementedException();
    }
}
