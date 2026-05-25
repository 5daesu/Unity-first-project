using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangePlayerStatAction", menuName = "ScriptableObjects/GameEventActions/ChangePlayerStatAction", order = 3)]
public class ChangePlayerStatAction : GameEventAction
{
    [SerializeField] private PlayerStat playerStat;
    [SerializeField] private int changedAmount;

    public override void Execute()
    {
        SingletonTable.singletonTable.piM.ChangePlayerStat(playerStat, changedAmount);
    }
}
