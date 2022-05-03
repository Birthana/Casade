using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class PowerUp : ScriptableObject
{
    [TextArea(3, 5)]
    public string description;

    public abstract void TriggerEffect();
}
