using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour {
    public AbilitySelector selector;
    [HideInInspector]
    public Ability ability;
    AbilityManager manager;
    AbilityTriggerable trigger;

    float nextTime;

    //UI
    Image image;
    Text info;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        info = GetComponentInChildren<Text>();
        manager = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityManager>();
        if (ability != null)
        {
            UpdateIcon();
            UpdateText(ability.name);
        }
    }

    void UpdateIcon()
    {
        image.sprite = ability.icon;
    }

    void UpdateText(string text)
    {
        info.text = text;
    }

    public void OnButtonClicked () {
        if (ability == null || Input.GetMouseButtonDown(2))
        {
            selector.ShowAbilityChoice(this);
        }
        else
        {
            trigger.TriggerAbility();
        }
    }

    public void AssignAbility(Ability newAbility)
    {
        ability = newAbility;
        UpdateIcon();
        UpdateText(ability.name);
    }

    public void AssignTrigger(AbilityTriggerable newTrigger)
    {
        trigger = newTrigger;
    }
}
