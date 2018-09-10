using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Sirenix.OdinInspector;
using UnityEngine;


[Serializable]
public class Attribute : BaseAttribute {
    [SerializeField]
    public Attributes name;

    //list of all modifiers to apply to this attribute
    private List<AttrModifier> _attrModifiers;
    protected ReadOnlyCollection<AttrModifier> attrModifiers;

    private bool isDirty = true;
    private float lastBaseValue = float.MinValue;
    private float _mostRecentValue;
    [ShowInInspector, ReadOnly] //for debugging
    float current; //for debugging
    private float _value;

    //overriding setter to dynamically calculate modified basevalue if it has been changed
    public float value {
        get {
            if (isDirty || baseValue != lastBaseValue)
            {
                lastBaseValue = baseValue;
                _mostRecentValue = CalculateFinalValue();
                isDirty = false;
            }
            current = _mostRecentValue; //for debugging but doesn't refresh in inspector
            return _mostRecentValue;
        }
    }

    //parameterless constructor
    public Attribute()
    {
        //Debug.Log(baseValue);
        _attrModifiers = new List<AttrModifier>();
        attrModifiers = _attrModifiers.AsReadOnly();
    }

    public virtual void AddModifier(AttrModifier mod)
    {
        isDirty = true;
        _attrModifiers.Add(mod);
        _attrModifiers.Sort(CompareModOrder);
    }

    //Comparaison fonction to be used to sort the modifier list, in AddModifier
    private int CompareModOrder(AttrModifier a, AttrModifier b)
    {
        if (a.order < b.order)
            return -1;
        else if (a.order > b.order)
            return 1;
        return 0; //if both have same order
    }

    public bool RemoveModifier(AttrModifier mod)
    {
        if(_attrModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }

    public bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;

        for (int i = _attrModifiers.Count - 1; i >= 0; i--)
        {
            if (_attrModifiers[i].source == source)
            {
                isDirty = true;
                _attrModifiers.RemoveAt(i);
                didRemove = true;
            }
        }

        return didRemove;
    }

    public bool RemoveModifierOfType(AttrModType modType)
    {
        bool didRemove = false;

        for (int i = _attrModifiers.Count - 1; i >= 0; i--)
        {
            if (_attrModifiers[i].type == modType)
            {
                isDirty = true;
                _attrModifiers.RemoveAt(i);
                didRemove = true;
                break;
            }
        }
        return didRemove;
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = baseValue;
        float sumPercentAdd = 0;

        for (int i = 0; i < _attrModifiers.Count; i++)
        {
            AttrModifier mod = _attrModifiers[i];

            if (mod.type == AttrModType.FLAT)
            {
                finalValue += mod.baseValue;
            }
            else if (mod.type == AttrModType.PERCENT_ADD)
            {
                sumPercentAdd += mod.baseValue;

                if(i + 1 >= _attrModifiers.Count || _attrModifiers[i +1].type != AttrModType.PERCENT_ADD)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }
            else if (mod.type == AttrModType.PERCENT_MULT)
            {
                finalValue *= 1 + mod.baseValue;
            }
        }

        // 12.0001f != 12f
        return (float)Math.Round(finalValue, 4);
    }
}
