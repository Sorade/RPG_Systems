using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;
using Sirenix.OdinInspector;


public interface ITargetable {
    TargetType targetType { get; set; }
    EffectManager effectManager { get; set; }
}
