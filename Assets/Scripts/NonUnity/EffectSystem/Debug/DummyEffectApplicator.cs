using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DummyEffectApplicator : MonoBehaviour, IEffectSource {
    [SerializeField]
    private CharacterStats stats;

    [SerializeField]
    private EffectManager target;
    [SerializeField]
    private Effect effect;

	// Use this for initialization
	void Start () {
	}
	
	public void ApplyEffect () {
        target.AddEffect(effect.CloneInitializedToSource(this));
    }

    public void RemoveEffect()
    {
        target.RemoveEffectsFromSource(this, triggerOnLeaveEffect: true);
    }

    public float GetEffectInputValue(Attributes attribute)
    {
        return 0f;
    }
}
