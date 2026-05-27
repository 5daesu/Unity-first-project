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
            SingletonTable.singletonTable.soM.UnSelectObject();
        }
        else if (mainButton.buttonState == 5)
        {
            MergeSelectedUnits();
        }
    }

    private void BuildCastle()
    {
        SingletonTable.singletonTable.soM.selectedObject.GetComponent<GameGrid>().BuildCastle();
        SingletonTable.singletonTable.piM.SpendCastleCost();
    }

    private void SummonUnit()
    {
        SingletonTable.singletonTable.soM.selectedObject.GetComponent<GameGrid>().Summon();
        SingletonTable.singletonTable.piM.SpendSummonCost();
    }


    private void MergeSelectedUnits()
    {
        List<GameObject> selectedUnits = SingletonTable.singletonTable.soM.GetSelectedUnits();
        List<UnitData> selectedUnitDataList = SingletonTable.singletonTable.soM.GetSelectedUnitDataList();

        UnitData resultUnitData;
        if (SingletonTable.singletonTable.piM.TryFindMergeResult(selectedUnitDataList, out resultUnitData) == false) return;
        if (resultUnitData == null || resultUnitData.unitPrefab == null) return;
        if (selectedUnits.Count == 0) return;

        GameGrid resultGrid = selectedUnits[0].transform.parent.GetComponent<GameGrid>();
        if (resultGrid == null) return;

        SingletonTable.singletonTable.soM.UnSelectObject();

        foreach (GameObject selectedUnit in selectedUnits)
        {
            if (selectedUnit == null) continue;

            GameGrid unitGrid = selectedUnit.transform.parent.GetComponent<GameGrid>();
            if (unitGrid != null)
            {
                unitGrid.summon = false;
                unitGrid.unit = null;
                unitGrid.RefreshSortingOrder();
            }

            Destroy(selectedUnit);
        }

        resultGrid.summon = true;
        resultGrid.unit = Instantiate(resultUnitData.unitPrefab, resultGrid.transform.position, Quaternion.identity, resultGrid.transform);

        UnitStatus resultUnitStatus = resultGrid.unit.GetComponent<UnitStatus>();
        if (resultUnitStatus != null) resultUnitStatus.unitData = resultUnitData;

        resultGrid.RefreshSortingOrder();
    }
}
