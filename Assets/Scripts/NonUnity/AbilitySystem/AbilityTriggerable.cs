using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

/// <summary>
/// Has to be valid for all characters
/// </summary>
public class AbilityTriggerable : SerializedMonoBehaviour {

    [SerializeField]
    Ability ability; //the ability which will be triggered

    [TitleGroup("External Target Handling")]
    public TargetHandler targetHandler;

    [Title("Self Targeting"), OdinSerialize]
    public ITargetable self;

    [HideInInspector]
    public Character character;

    public void Init(Ability linkedAbility, ITargetable Self)
    {
        self = Self;
        targetHandler = GetComponentInParent<TargetHandler>();
        character = GetComponentInParent<Character>();
        ability = linkedAbility;
        ability.trigger = this;

        if (targetHandler == null) { Debug.Log(transform.parent.name + " targetHandler not found.");   }
        if (character == null) { Debug.Log(transform.parent.name + " character not found."); }
    }

    public void TriggerAbility()
    {
        if (ability.targetEffects.Length == 0) // if no target but self is needed trigger ability
        {
            ability.ApplyOnSelf(self.effectManager);
        }
        else // search for target
        {
            targetHandler.SearchTarget(ability.targetTypes); //launches the targethandler search for suitable targets
            targetHandler.OnTargetFound.AddListener(TriggerOnTargetFound);// start listening to target handler
        }
    }

    // This is triggered by the target handler when a suitable target has been found
    void TriggerOnTargetFound(ITargetable[] targets)
    {
        Debug.Log("target found. Applying Ability");
        targetHandler.OnTargetFound.RemoveListener(TriggerOnTargetFound);// stops listening to target handler
        //When a valid target exists trigger the ability effects
        ability.ApplyOnSelf(self.effectManager); //Applies the self effects to caster
        TriggerOnExternal(targets); // applies target effects on targets
    }

    void TriggerOnExternal(ITargetable[] targets)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            ability.ApplyOnTarget(targets[i].effectManager);
        }
    }
}
