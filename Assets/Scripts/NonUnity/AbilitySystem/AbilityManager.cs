using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;
using Sirenix.OdinInspector;

public class AbilityManager : SerializedMonoBehaviour
{
    [OdinSerialize]
    public ITargetable self;
    public Ability[] accessibleAbilities;
    public AbilityTriggerable[] triggers;
    [SerializeField]
    GameObject holder;

	// Use this for initialization
	void Start () {
        triggers = new AbilityTriggerable[accessibleAbilities.Length];
        AddAbilities();
    }

    void AddAbilities()
    {
        for (int i = 0; i < accessibleAbilities.Length; i++)
        {
            AbilityTriggerable trigger = holder.AddComponent<AbilityTriggerable>();
            accessibleAbilities[i] = Instantiate(accessibleAbilities[i]); // Creates a local copy of the ability
            trigger.Init(accessibleAbilities[i], self);
            triggers[i] = trigger;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    [Button]
    void UnlockAbilities()
    {
        for (int i = 0; i < accessibleAbilities.Length; i++)
        {

            accessibleAbilities[i].unlocked = true;
        }
    }
}
