using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public static InGameUI inGameUI;    //singleton
    public MainButton mainButton;

    private void Awake()
    {
        if (InGameUI.inGameUI == null)
        {
            InGameUI.inGameUI = this;
        }
    }

    public void OnClickMainButton()  //need revision : the button will be used for multipurpose 
    {
        if (mainButton.buttonState == 1)
        {
            BuildCastle();
        }
        else if (mainButton.buttonState == 3)
        {
            SummonUnit();
            SingletonTable.singletonTable.soM.UnSelsectObject();  //There is CheckButtonState
        }
    }

    private void BuildCastle()
    {
        SingletonTable.singletonTable.soM.selectedObject.GetComponent<Grid>().BuildCastle();
        SingletonTable.singletonTable.piM.SpendCastleCost();
    }

    private void SummonUnit()
    {
        SingletonTable.singletonTable.soM.selectedObject.GetComponent<Grid>().Summon();
        SingletonTable.singletonTable.piM.SpendSummonCost();
    }

}
