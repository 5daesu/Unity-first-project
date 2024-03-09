using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent : ScriptableObject
{
    public int gameEventCode;
    public string gameEventName;
    public Sprite gameEventSprite;
    public string gameEventBodyText;

    public abstract bool PrimaryCondition();
    public abstract float SecondaryCondition();   // return value range is 0 ~ 1, return value is probability, if probability is 0, this cant be in availableGameEventsList.
    public abstract void StartGameEvent();
}