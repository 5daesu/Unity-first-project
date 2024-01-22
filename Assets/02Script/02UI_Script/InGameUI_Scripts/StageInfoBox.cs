using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoBox : MonoBehaviour
{
    public StageManager stM;
    public Text curStageText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStageValue();
    }

    public void UpdateStageValue()
    {
        if (stM.curStage >= 10) curStageText.text = stM.curStage + " Stage";
        else curStageText.text = "0" + stM.curStage + " Stage";
    }
}
