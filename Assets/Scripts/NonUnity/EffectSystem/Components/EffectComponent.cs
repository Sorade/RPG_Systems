using System;
using UnityEngine;
using Sirenix.OdinInspector;

public class EffectComponent : ScriptableObject
{
    public Effect parentEffect;
    public EffectApplicatorType applicator;

    [Space]
    public readonly bool useSourceAttribute;
    [ShowIf("useSourceAttribute"), InfoBox("The source attribute value multiplied by the damping coef and added to the base value", "useSourceAttribute")]
    public Attributes attribute;
    [ShowIf("useSourceAttribute"), SerializeField]
    private float damping = 1f;

    [HideInInspector]
    public float sourceInput = 0f;

    [Space]
    [SerializeField]
    protected float baseValue;
    public float Value { get { return baseValue + sourceInput * damping; } }

    // The effect that will be added to the AttributeEffect's delegates
    // on creation
    public virtual void Apply(EffectManager target) { }
}
