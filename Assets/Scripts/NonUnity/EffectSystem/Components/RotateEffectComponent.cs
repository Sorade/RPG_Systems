using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Effect/Components/Rotate")]
public class RotateEffectComponent : EffectComponent {

    // overrides the component apply method to handle
    // custom behaviour
    public override void Apply(EffectManager target)
    {
        target.transform.Rotate(Vector3.forward, 1f * baseValue);
    }
}
