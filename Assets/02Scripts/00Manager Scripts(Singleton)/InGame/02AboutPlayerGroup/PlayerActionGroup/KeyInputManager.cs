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
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (ManagerGrouping.managerGrouping.uwM.inventoryWindow.isActive == false) ManagerGrouping.managerGrouping.uwM.inventoryWindow.OpenWindow();
            else ManagerGrouping.managerGrouping.uwM.inventoryWindow.CloseWindow();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (ManagerGrouping.managerGrouping.uwM.questWindow.isActive == false) ManagerGrouping.managerGrouping.uwM.questWindow.OpenWindow();
            else ManagerGrouping.managerGrouping.uwM.questWindow.CloseWindow();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (ManagerGrouping.managerGrouping.uwM.detailedPlayerResourceWindow.isActive == false) ManagerGrouping.managerGrouping.uwM.detailedPlayerResourceWindow.OpenWindow();
            else ManagerGrouping.managerGrouping.uwM.detailedPlayerResourceWindow.CloseWindow();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if
            //else
            ManagerGrouping.managerGrouping.uwM.topTogglingWindow.CloseWindow();

            Debug.Log("esc");
        }
    }
}
