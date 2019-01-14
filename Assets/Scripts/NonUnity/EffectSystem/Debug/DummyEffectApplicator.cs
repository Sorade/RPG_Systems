using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DummyEffectApplicator : MonoBehaviour, IEffectSource {
    //delegate definition
    public delegate void EffectSourceRemover(IEffectSource source, bool triggerOnLeave);
    // delegate which store the effectRemovers from various sources
    public EffectSourceRemover _effectRemovers;


    [SerializeField]
    private CharacterStats stats;

    [SerializeField]
    private EffectManager target;
    [SerializeField]
    private Effect effect;

    // Use this for initialization
    void Start () {
	}

    void OnDestroy()
    {
        _effectRemovers = null;
    }
	
	public void ApplyEffect () {
        target.AddEffect(effect.CloneInitializedToSource(this));
        //add the removeFromSource method of the target's EffectManager to the delegate
        // _effectRemovers to make sure that the removing can be triggered even if target has changed
        _effectRemovers += target.RemoveEffectsFromSource;
    }

    public float GetEffectInputValue(Attributes attribute)
    {
        return 0f;
    }

    public void RemoveEffects()
    {
        if (_effectRemovers != null)
        {
            _effectRemovers(this, true);
        };
    }
}
