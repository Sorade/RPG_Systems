using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySelector : MonoBehaviour {
    AbilityManager manager;
    [SerializeField]
    GameObject abilitySlot;
    [SerializeField]
    GameObject contents;

    public AbilityCooldown currentAbCooldown; 

    /*public delegate void OnAbilitySelected(int index);

    public static OnAbilitySelected onAbilitySelectedCallback;

    private void OnEnable()
    {
        onAbilitySelectedCallback += SelectAbility;
    }

    private void OnDisable()
    {
        onAbilitySelectedCallback -= SelectAbility;
    }*/

    // Use this for initialization
    void Start () {
        manager = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityManager>();
        gameObject.SetActive(false);
    }

    public void ShowAbilityChoice(AbilityCooldown currentCooldown)
    {
        gameObject.SetActive(true);
        currentAbCooldown = currentCooldown;

        int abilityIndex = 0;
        for (int i = 0; i < manager.triggers.Length; i++)
        {
            if (manager.accessibleAbilities[i].unlocked)
            {
                GameObject newSlot = Instantiate(abilitySlot, contents.transform);
                newSlot.GetComponent<Image>().sprite = manager.accessibleAbilities[i].icon;
                AbilitySlotSelector slot = newSlot.GetComponent<AbilitySlotSelector>();
                slot.abilityIndex = abilityIndex;
                slot.selector = this;
            }
            abilityIndex++;
        }
    }

    public void AssignAbilityToCooldownButton(int index)
    {
        Ability ability = manager.accessibleAbilities[index];
        if (ability != null)
        {
            currentAbCooldown.AssignAbility(ability);
            currentAbCooldown.AssignTrigger(manager.triggers[index]);
        }
        currentAbCooldown = null;
        gameObject.SetActive(false);
        HideAbilityChoice();
    }

    void HideAbilityChoice()
    {
        foreach (Transform child in contents.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
