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
            ManagerGrouping.managerGrouping.soM.UnSelsectObject();  //There is CheckButtonState
        }
    }

    private void BuildCastle()
    {
        ManagerGrouping.managerGrouping.soM.selectedObject.GetComponent<Grid>().BuildCastle();
        ManagerGrouping.managerGrouping.piM.SpendCastleCost();
    }

    private void SummonUnit()
    {
        ManagerGrouping.managerGrouping.soM.selectedObject.GetComponent<Grid>().Summon();
        ManagerGrouping.managerGrouping.piM.SpendSummonCost();
    }

}
