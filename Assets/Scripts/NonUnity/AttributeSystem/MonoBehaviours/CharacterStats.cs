using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class CharacterStats : MonoBehaviour {

    [Header("Gages")]
    public AttributeHealth health;

    [Header("Core Attributes")]
    public Attribute strength;
    public Attribute agility;
    public Attribute intelligence;

    [Header("Damage Attributes")]
    public Attribute PhysicalDamage;
    public Attribute FireDamage;
    public Attribute ColdDamage;
    public Attribute PoisonDamage;


    [HideInInspector]
    public Attribute testAttribute;

    public Dictionary<Attributes, Attribute> attributes;


    private void Start()
    {
        health = new AttributeHealth(strength, health.baseValue);
        testAttribute = health;
        Debug.Log(gameObject.name + " start health: " + health.value);

        CreateAttributeDictionary();
    }

    void CreateAttributeDictionary()
    {
        attributes = new Dictionary<Attributes, Attribute>();

        AddAttributeDictionaryEntry(strength, Attributes.STRENGHT);
        AddAttributeDictionaryEntry(agility, Attributes.AGILITY);
        AddAttributeDictionaryEntry(intelligence, Attributes.INTELLIGENCE);
        AddAttributeDictionaryEntry(health, Attributes.HEALTH);
        AddAttributeDictionaryEntry(PhysicalDamage, Attributes.DMG_PHYS);
        AddAttributeDictionaryEntry(FireDamage, Attributes.DMG_FIRE);
        AddAttributeDictionaryEntry(ColdDamage, Attributes.DMG_COLD);
        AddAttributeDictionaryEntry(PoisonDamage, Attributes.DMG_POISON);


    }

    void AddAttributeDictionaryEntry(Attribute attribute, Attributes expectedName)
    {
        if (attribute.name == expectedName)
        {
            attributes.Add(expectedName, attribute);
        }
        else
        {
            Debug.LogError(expectedName.ToString().ToLower() + " attribute's name not set correctly. " + attribute.name + " found.");
        }
    }


    // Methods to Add Modifiers to Attributes
    #region
    public void AddAttributeModifier(float value, AttrModType type, object source)
    {
        testAttribute.AddModifier(new AttrModifier(value, AttrModType.FLAT, this));
    }

    public void AddAttributeModifier(float value, AttrModType type)
    {
        testAttribute.AddModifier(new AttrModifier(value, AttrModType.FLAT));
    }

    // All methods from here to region end only have one parameter
    // to be used with UI Buttons
    public void AddFlatModifier(int value)
    {
        testAttribute.AddModifier(new AttrModifier(value, AttrModType.FLAT, this));
        Debug.Log(testAttribute.value);
    }

    public void AddPercentAddModifier(float value)
    {
        testAttribute.AddModifier(new AttrModifier(value, AttrModType.PERCENT_ADD, this));
        Debug.Log(testAttribute.value);
    }

    public void AddPercentMultModifier(float value)
    {
        testAttribute.AddModifier(new AttrModifier(value, AttrModType.PERCENT_MULT, this));
        Debug.Log(testAttribute.value);
    }

    public void RemoveAllModifiers()
    {
        testAttribute.RemoveAllModifiersFromSource(this);
        Debug.Log(testAttribute.value);
    }
    #endregion

    // Special Methods
    public void TakeDamage(float amount, DamageType type, AttrModType modType)
    {
        // A switch can be implemented here to reduce the amount based on the
        // damage type and the associated resitances
        health.AddModifier(new AttrModifier(-amount, modType));
        Debug.Log(gameObject.name +" health " + health.value +" remaining after "+ amount + " damage delt");
    }

    public void Heal(float amount, AttrModType type)
    {
        // Checks if a modifier equal to the negative of the amount to heal can be found
        if (health.RemoveModifier(new AttrModifier(-amount, type)) == false)
        {
            // If not adds the amount to heal as a new modifier
            health.AddModifier(new AttrModifier(amount, type));
        }
        Debug.Log(gameObject.name + " health of " + health.value + " after " + amount + " healing.");
    }

    //Custom Editor
    /*#region
    [ShowIf("IsDependentAttribute", true)]
    public DependentAttribute editedDependent;

    [HideIf("IsDependentAttribute", true)]
    public Attribute editedIndependent;

    public bool IsDependentAttribute = true;
    
    [Button]
    void CreateDependentAttribute()
    {

    }

    [Button]
    void CreateAttribute()
    {

    }
    #endregion*/
}
