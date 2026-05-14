using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoticeGameEvent", menuName = "ScriptableObjects/GameEvents/NoticeGameEvent", order = 0)]
public class NoticeGameEvent : GameEvent
{
    [SerializeField] private bool primaryCondition = true;
    [SerializeField, Range(0f, 1f)] private float secondaryCondition = 1f;

    public override bool PrimaryCondition()
    {
        return primaryCondition;
    }

    public override float SecondaryCondition()
    {
        return secondaryCondition;
    }
}
