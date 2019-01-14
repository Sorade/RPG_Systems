using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectSource {
    float GetEffectInputValue(Attributes attribute);

    void RemoveEffects();

 }

