using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class TargetDetect : MonoBehaviour
{
    [Serializable]
    public class TargetEvent : UnityEvent<ITargetable> { }

    [DrawWithUnity]
    public static TargetEvent EnteredTarget = new TargetEvent();
    [DrawWithUnity]
    public static TargetEvent ExitedTarget = new TargetEvent();
    [DrawWithUnity]
    public static TargetEvent ClickedTarget = new TargetEvent();

    public ITargetable target;

    void Start()
    {
        target = GetComponent<ITargetable>();
    }

    public void OnMouseDown()
    {
        ClickedTarget.Invoke(target);
    }

    public void OnMouseEnter()
    {
        EnteredTarget.Invoke(target);
    }

    public void OnMouseExit()
    {
        ExitedTarget.Invoke(target);
    }
}
