using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Effect/New Effect")]
public class Effect : ScriptableObject
{
    //delegate definition
    public delegate void EffectApplicator(EffectManager target);

    //delegates creations for entry, tick and leave effects
    //Entry effects are called when the effect is added
    public EffectApplicator _entryEffect;

    //Tick effects are like DoTs, which occur every (_tickInterval) time that applies something to the Kat
    public EffectApplicator _tickEffect;
    [HideIf("isPermanent")]
    public float _tickInterval;
    [HideInInspector]
    public float _nextTickTime = 0f;

    //OnLeave effects are called when the effect is removed - through purge or expiry
    public EffectApplicator _onLeaveEffect;

    //Properties handling timed effects
    protected int currTickCount = 0;
    [HideIf("isPermanent"), SerializeField]
    protected float _duration;
    private float _expiryTime;

    // Generic attributes
    public bool isPermanent; //check wether the effect is removed using the duration attribute
    public EffectName id;
    public IEffectSource source;

    [ListDrawerSettings(HideAddButton =true)]
    public EffectComponent[] components;

    // Methods //
    private void Initialize()
    {
        SetEffectToComponents();
        SetInputsFromSource();
        _expiryTime = Time.time + _duration;
        SetTickedTime(Time.time + _tickInterval);
        AddApplicators();
    }

    private void OnDestroy()
    {
        RemoveApplicators();
    }

    public bool HasExpired(float currTime)
    {
        if (currTime > _expiryTime) return true;
        return false;
    }


    public void SetTickedTime(float t)
    {
        _nextTickTime = t;
    }

    //My System Methods
    void SetEffectToComponents()
    //Assigns the effect to all components
    {
        for (int i = 0; i < components.Length; i++)
        {
            components[i].parentEffect = this;
        }
    }

    public void SetInputsFromSource()
        //Assigns the sourceInputs to all components that require it
    {
        for (int i = 0; i < components.Length; i++)
        {
            if (source != null && components[i].useSourceAttribute)
            {
                components[i].sourceInput = source.GetEffectInputValue(components[i].attribute);
            }
        }
    }

    private Effect Clone()
    {
        Effect clone = Instantiate(this); //clones the effect
        for (int i = 0; i < components.Length; i++)
        {
            clone.components[i] = Instantiate(components[i]); //clones all its components
        }
        return clone;
    }

    public Effect CloneInitializedToSource(IEffectSource source)
    {
        Effect clone = Clone();
        clone.source = source;
        clone.Initialize();
        return clone;
    }

    public void AddApplicators()
    {
        for (int i = 0; i < components.Length; i++)
        {
            EffectApplicatorType type = components[i].applicator;

            switch (type)
            {
                case EffectApplicatorType.ENTRY:
                    _entryEffect += components[i].Apply;
                    break;
                case EffectApplicatorType.TICK:
                    _tickEffect += components[i].Apply;
                    break;
                case EffectApplicatorType.LEAVE:
                    _onLeaveEffect += components[i].Apply;
                    break;
            }
        }
    }

    public void RemoveApplicators()
    {
        for (int i = 0; i < components.Length; i++)
        {
            EffectApplicatorType type = components[i].applicator;

            switch (type)
            {
                case EffectApplicatorType.ENTRY:
                    _entryEffect -= components[i].Apply;
                    break;
                case EffectApplicatorType.TICK:
                    _tickEffect -= components[i].Apply;
                    break;
                case EffectApplicatorType.LEAVE:
                    _onLeaveEffect -= components[i].Apply;
                    break;
            }
        }
    }
}
