using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BumperCropEvent", menuName = "ScriptableObjects/GameEvents/BumperCropEvent", order = 1)]
public class BumperCropGameEvent : NoticeGameEvent
{
    public override bool PrimaryCondition()
    {
        return true;
    }
    public override float SecondaryCondition()
    {
        return 1;
    }
    public override void StartGameEvent()
    {
        Debug.Log("Get a 10 Gold!");
        ManagerGrouping.managerGrouping.piM.ChangeMoney(10);
    }
}