using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySlotSelector : MonoBehaviour {
    [HideInInspector]
    public AbilitySelector selector;
    [HideInInspector]
    public int abilityIndex;

	public void SelectAbility () {
        selector.AssignAbilityToCooldownButton(abilityIndex);
    }
}
