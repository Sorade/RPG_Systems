using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class EffectManager : MonoBehaviour
{
    private float currTime;

    //public enum StatusType { None, BURN, FREEZE, BLEED, POISON, PARALYZE, SLEEP, FURY, ARMOR, HASTE, REGEN, WARD, FOCUS};

    [ShowInInspector, ReadOnly]
    private List<Effect> existingEffects = new List<Effect>();
    private List<EffectName> existingEffectTypes = new List<EffectName>();

    public CharacterStats stats;
    //private GraphicEffectManager graphicEffectManager;

    void Start()
    {
        stats = gameObject.GetComponent<CharacterStats>();
        //graphicEffectManager = this.gameObject.GetComponent<GraphicEffectManager>();
    }

    void Update()
    {
        currTime = Time.time;
        ProcAllTickingCombatEffects();
        TrimExpiredEffects();
    }

    private void TrimExpiredEffects()
    {
        //traverse buffs list and remove all the expired ones
        for (int i = existingEffects.Count - 1; i >= 0; i--)
        {
            if (existingEffects[i].HasExpired(currTime) && !existingEffects[i].isPermanent)
            {
                //				Debug.Log (existingCombatEffects [i]._effectCode.ToString () + " has expired!");
                if (existingEffects[i]._onLeaveEffect != null) existingEffects[i]._onLeaveEffect(this);
                Debug.Log(existingEffects[i].name + " expired and removed.");
                existingEffects.RemoveAt(i);
            }
        }
    }

    private void ProcAllTickingCombatEffects()
    {
        for (int i = 0; i < existingEffects.Count; i++)
        {
            if (existingEffects[i]._tickEffect != null)
            {   //If Effect has a tick component
                
                if (existingEffects[i]._nextTickTime <= currTime)
                {   //If the tick is due to proc
                    existingEffects[i].SetTickedTime(existingEffects[i]._nextTickTime + existingEffects[i]._tickInterval);
                    existingEffects[i]._tickEffect(this);    //Call tick effect
                }
            }
        }
    }

    public void AddEffect(Effect effect)
    {
        existingEffects.Add(effect);
        if (effect._entryEffect != null)
        {
            effect._entryEffect(this);
        }
    }

    //Will remove all the effects of that EffectName (e.g. all BURN)
    public void RemoveEffectsByID(EffectName id, bool triggerOnLeaveEffect)
    {
        if (existingEffectTypes.Contains(id))
        {
            //existingEffectTypes.Remove (code);
            for (int i = 0; i < existingEffects.Count; i++)
            {
                //Looping through all CombatEffects in List to find one that matches the type; highly INEFFICIENT
                if (existingEffects[i].id == id)
                {
                    if (triggerOnLeaveEffect && existingEffects[i]._onLeaveEffect != null) existingEffects[i]._onLeaveEffect(this);
                    existingEffects.RemoveAt(i);
                    //hasBuffListChanged = true;
                    //RebuildExistingEffectTypesList();
                }
            }
        }
    }

    //Will Remove all effects from a given source
    public void RemoveEffectsFromSource(IEffectSource source, bool triggerOnLeaveEffect)
    {
        for (int i = 0; i < existingEffects.Count; i++)
        {
            if (existingEffects[i].source == source)
            {
                if (triggerOnLeaveEffect && existingEffects[i]._onLeaveEffect != null) existingEffects[i]._onLeaveEffect(this);
                existingEffects.RemoveAt(i);
            }
        }
    }
}