using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Effect/Components/Attribute")]
public class AttributeEffectComponent : EffectComponent
{
    [SerializeField]
    public AttrModType modifierType;

    public Attributes targetAttribute;
    // overrides the component apply method to handle
    // custom behaviour
    public override void Apply(EffectManager target)
    {
        if (target.stats.attributes.ContainsKey(targetAttribute))
        {
            target.stats.attributes[targetAttribute].AddModifier(new AttrModifier(Value, modifierType, parentEffect)); //the stats class of the character will handle all the modifiers, via damage calculator class
            parentEffect.SetInputsFromSource(); //Refreshes inputs from source
            Debug.Log(target.gameObject.name + "'s " + targetAttribute.ToString().ToLower() + " changed by " + Value + " " + modifierType.ToString().ToLower() + ". Current " + target.stats.attributes[targetAttribute].value);
        }
        else
        {
            Debug.LogWarning(targetAttribute + " not added to " + target.name + " CharacterStats. Effect eot applied.");
        }
    }
}
