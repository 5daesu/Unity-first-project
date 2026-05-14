using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeMoneyAction", menuName = "ScriptableObjects/GameEventActions/ChangeMoneyAction", order = 1)]
public class ChangeMoneyAction : GameEventAction
{
    [SerializeField] private int changedAmount;

    public override void Execute()
    {
        SingletonTable.singletonTable.piM.ChangeMoney(changedAmount);
    }
}
