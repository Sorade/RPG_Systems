using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/Components/Prefab Spawner")]
public class PrefabComponent : EffectComponent
{
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    Vector3 offset;
    // overrides the component apply method to handle
    // custom behaviour
    public override void Apply(EffectManager target)
    {
        Instantiate(prefab, target.transform.position + offset, target.transform.rotation);
    }
}
