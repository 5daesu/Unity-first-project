using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeHpAction", menuName = "ScriptableObjects/GameEventActions/ChangeHpAction", order = 2)]
public class ChangeHpAction : GameEventAction
{
    [SerializeField] private int changedAmount;

    public override void Execute()
    {
        SingletonTable.singletonTable.piM.ChangeHp(changedAmount);
    }
}
