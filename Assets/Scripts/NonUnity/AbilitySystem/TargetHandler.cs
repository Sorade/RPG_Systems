using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

/// <summary>
/// Is Player Character Specific
/// </summary>
public class TargetHandler : SerializedMonoBehaviour {

    [Serializable]
    public class TargetablesEvent : UnityEvent<ITargetable[]> { }
    [DrawWithUnity]
    public TargetablesEvent OnTargetFound = new TargetablesEvent();

    TargetType expectedTargetType;
    public bool isValidTarget;
    public List<ITargetable> targets = new List<ITargetable>();


    private void Start()
    {
    }

    public void CheckTargetValidity(ITargetable targeted)
    {
        ITargetable target = targeted;
        isValidTarget =  expectedTargetType.HasFlag(target.targetType);
        if (isValidTarget)
        {
            if (targets.Contains(target))
            {
                targets.Remove(target); // Assumes player wants to deselect the target
                isValidTarget = false; // prevents the ability from triggering
                Debug.Log("Ability trigger aborted");
            }
            else
            {
                targets.Add(target);
            }
        }
        else
        {
            Debug.Log("... target unsuitable");
        }
    }

    public void SearchTarget(TargetType targetType) {
        expectedTargetType = targetType; // sets the target type to search in order to trigger active ability
        TargetDetect.ClickedTarget.AddListener(CheckTargetValidity); //Starts listening for clicks on potential target cards
        StartCoroutine("WaitForSuitableTarget");
	}    
	
	IEnumerator WaitForSuitableTarget()
    {
        Debug.Log("Waiting for target selection ...");
        while (!isValidTarget)
        {
            yield return null;
        }
        TargetDetect.ClickedTarget.RemoveListener(CheckTargetValidity); //stops listening for clicks on potential target cards
        OnTargetFound.Invoke(targets.ToArray()); // For all the AbilityTrigerables to actually trigger the ability
        targets.Clear();
        isValidTarget = false;
    }
}
