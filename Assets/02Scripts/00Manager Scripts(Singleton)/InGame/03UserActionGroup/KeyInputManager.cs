using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputManager : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(ManagerGrouping.managerGrouping.ibM.inputbindingDict[UserAction.Toggle_Inventory_Window]))
        {
            if (ManagerGrouping.managerGrouping.uwM.inventoryWindow.isActive == false) ManagerGrouping.managerGrouping.uwM.inventoryWindow.OpenWindow();
            else ManagerGrouping.managerGrouping.uwM.inventoryWindow.CloseWindow();
        }
        else if (Input.GetKeyDown(ManagerGrouping.managerGrouping.ibM.inputbindingDict[UserAction.Toggle_Quest_Window]))
        {
            if (ManagerGrouping.managerGrouping.uwM.questWindow.isActive == false) ManagerGrouping.managerGrouping.uwM.questWindow.OpenWindow();
            else ManagerGrouping.managerGrouping.uwM.questWindow.CloseWindow();
        }
        else if (Input.GetKeyDown(ManagerGrouping.managerGrouping.ibM.inputbindingDict[UserAction.Toggle_DetailedPlayerInfo_Window]))
        {
            if (ManagerGrouping.managerGrouping.uwM.detailedPlayerInfoWindow.isActive == false) ManagerGrouping.managerGrouping.uwM.detailedPlayerInfoWindow.OpenWindow();
            else ManagerGrouping.managerGrouping.uwM.detailedPlayerInfoWindow.CloseWindow();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ManagerGrouping.managerGrouping.uwM.topTogglingWindow != null) ManagerGrouping.managerGrouping.uwM.topTogglingWindow.CloseWindow();
            else 

            Debug.Log("esc");
        }
    }
}