using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;


public class Character : SerializedMonoBehaviour, ITargetable {
    [ReadOnly]
    public CharacterStats stats;
    TargetHandler targetHandler;
    //The three lines below show how to implement properties to get them to serialize properly in the inspector
    [SerializeField]
    private TargetType _targetType;
    public TargetType targetType { get { return _targetType; } set { _targetType = value; } }
    [ReadOnly]
    public EffectManager effectManager { get; set; }

    void Start()
    {
        effectManager = GetComponent<EffectManager>();
        stats = GetComponent<CharacterStats>();
    }    
}
