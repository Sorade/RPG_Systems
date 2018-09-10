using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Links all the templates to an AttributeEffectName enum
// Any AttributeEffect can then easily be accessed
public class EffectFactory
{
    /*public static IDictionary<EffectName, EffectTemplate> library = new Dictionary<EffectName, EffectTemplate>();

    public static Effect InstantiateEffect(EffectName id)
    {
        Effect newEffect = new Effect();
        RegisterEffectComponents(newEffect._entryEffect, library[id].initialEffects);
        RegisterEffectComponents(newEffect._tickEffect, library[id].tickEffects);
        RegisterEffectComponents(newEffect._onLeaveEffect, library[id].onLeaveEffects);
        newEffect.SetTimer(library[id].duration, library[id].tickInterval);

        return newEffect;
    }

    public static void DestroyEffect(Effect attribute)
    {
        UnRegisterEffectComponents(attribute._entryEffect, library[attribute.id].initialEffects);
        UnRegisterEffectComponents(attribute._tickEffect, library[attribute.id].tickEffects);
        UnRegisterEffectComponents(attribute._onLeaveEffect, library[attribute.id].onLeaveEffects);
        // attribute.Dispose(); // need to destroy unused effect
    }

    static void RegisterEffectComponents(Effect.EffectApplicator applicator, List<AttributeEffectComponent> components)
    {
        for (int i = 0; i < components.Count; i++)
        {
            applicator += components[i].Apply;
        }
    }

    static void UnRegisterEffectComponents(Effect.EffectApplicator applicator, List<AttributeEffectComponent> components)
    {
        for (int i = 0; i < components.Count; i++)
        {
            applicator -= components[i].Apply;
        }
    }*/
}
