using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class BaseAttribute {
    [SerializeField]
    private float _baseValue;
    [SerializeField]
    public float baseValue { get { return _baseValue; } set { _baseValue = value;} }

}
