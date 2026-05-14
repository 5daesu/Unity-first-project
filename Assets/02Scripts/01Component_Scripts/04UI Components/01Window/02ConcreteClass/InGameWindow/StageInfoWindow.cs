using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoWindow : NonTogglingWindow
{
    public Text curStageText;

    public override void UpdateWindowContent()
    {
        if (SingletonTable.singletonTable.gpM.curRound >= 10) curStageText.text = SingletonTable.singletonTable.gpM.curRound + " Round";
        else curStageText.text = "0" + SingletonTable.singletonTable.gpM.curRound + " Round";
    }
}