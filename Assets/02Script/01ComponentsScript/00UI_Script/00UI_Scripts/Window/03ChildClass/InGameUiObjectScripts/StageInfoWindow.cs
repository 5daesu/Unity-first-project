using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoWindow : NonTogglingWindow
{
    public Text curStageText;

    public override void UpdateWindowContent()
    {
        if (ManagerGrouping.managerGrouping.stM.curStage >= 10) curStageText.text = ManagerGrouping.managerGrouping.stM.curStage + " Stage";
        else curStageText.text = "0" + ManagerGrouping.managerGrouping.stM.curStage + " Stage";
    }
}