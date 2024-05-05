using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoWindow : NonTogglingWindow
{
    public Text curStageText;

    public override void UpdateWindowContent()
    {
        if (ManagerGrouping.managerGrouping.gpM.curRound >= 10) curStageText.text = ManagerGrouping.managerGrouping.gpM.curRound + " Round";
        else curStageText.text = "0" + ManagerGrouping.managerGrouping.gpM.curRound + " Round";
    }
}