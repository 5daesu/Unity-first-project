using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EconomicBoomEvent", menuName = "ScriptableObjects/GameEvents/EconomicBoomEvent", order = 2)]
public class EconomicBoomGameEvent : NoticeGameEvent
{
    public override bool PrimaryCondition()
    {
        return true;
    }
    public override float SecondaryCondition()
    {
        return 0.5f;
    }

    public override void StartGameEvent()
    {
        ManagerGrouping.managerGrouping.piM.ChangeMoney(50);
    }
}