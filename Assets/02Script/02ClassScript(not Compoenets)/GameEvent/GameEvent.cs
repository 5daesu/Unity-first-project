using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent : MonoBehaviour
{
    public string eventName;

    public abstract void ResetData();
    public abstract void StartEvent();
}