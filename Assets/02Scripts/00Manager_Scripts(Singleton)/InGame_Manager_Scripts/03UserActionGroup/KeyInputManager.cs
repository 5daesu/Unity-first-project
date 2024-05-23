using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputManager : MonoBehaviour        //it is a set with KeyBindingManager
{
    private int inputDisabledWinodwStack = 0;           //value = 0 : initial state    value > 0 : input is disabled

    public void InputDisabledWindowStackUp()
    {
        inputDisabledWinodwStack++;
    }

    public void InputDisabledWindowStackDown()
    {
        inputDisabledWinodwStack--;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))   //ESC isn't influenced by inputDisabledWindow
        {
            if (ManagerGrouping.managerGrouping.uwM.topTogglingWindow != null) ManagerGrouping.managerGrouping.uwM.topTogglingWindow.CloseWindow();
            else
            {
                Debug.Log("esc!");
                ManagerGrouping.managerGrouping.uwM.gameSettingScreen.OpenWindow();
            }
        }

        if (inputDisabledWinodwStack == 0)
        {

            if (Input.GetKeyDown(ManagerGrouping.managerGrouping.kbM.keybindingDict[UserAction.Toggle_Inventory_Window]))
            {
                if (ManagerGrouping.managerGrouping.uwM.inventoryWindow.isActive == false) ManagerGrouping.managerGrouping.uwM.inventoryWindow.OpenWindow();
                else ManagerGrouping.managerGrouping.uwM.inventoryWindow.CloseWindow();
            }

            else if (Input.GetKeyDown(ManagerGrouping.managerGrouping.kbM.keybindingDict[UserAction.Toggle_Quest_Window]))
            {
                if (ManagerGrouping.managerGrouping.uwM.questWindow.isActive == false) ManagerGrouping.managerGrouping.uwM.questWindow.OpenWindow();
                else ManagerGrouping.managerGrouping.uwM.questWindow.CloseWindow();
            }

            else if (Input.GetKeyDown(ManagerGrouping.managerGrouping.kbM.keybindingDict[UserAction.Toggle_DetailedPlayerInfo_Window]))
            {
                if (ManagerGrouping.managerGrouping.uwM.detailedPlayerInfoWindow.isActive == false) ManagerGrouping.managerGrouping.uwM.detailedPlayerInfoWindow.OpenWindow();
                else ManagerGrouping.managerGrouping.uwM.detailedPlayerInfoWindow.CloseWindow();
            }

        }
        else
        {
            //Need something for User
        }
        
    }
}