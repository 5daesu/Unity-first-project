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
            if (SingletonTable.singletonTable.uwM.topTogglingWindow != null) SingletonTable.singletonTable.uwM.topTogglingWindow.CloseWindow();
            else
            {
                Debug.Log("esc!");
                SingletonTable.singletonTable.uwM.gameSettingScreen.OpenWindow();
            }
        }

        if (inputDisabledWinodwStack == 0)
        {

            if (Input.GetKeyDown(SingletonTable.singletonTable.kbM.curKeybindingSet[UserAction.Toggle_Inventory_Window]))
            {
                if (SingletonTable.singletonTable.uwM.inventoryWindow.isActive == false) SingletonTable.singletonTable.uwM.inventoryWindow.OpenWindow();
                else SingletonTable.singletonTable.uwM.inventoryWindow.CloseWindow();
            }

            else if (Input.GetKeyDown(SingletonTable.singletonTable.kbM.curKeybindingSet[UserAction.Toggle_Quest_Window]))
            {
                if (SingletonTable.singletonTable.uwM.questWindow.isActive == false) SingletonTable.singletonTable.uwM.questWindow.OpenWindow();
                else SingletonTable.singletonTable.uwM.questWindow.CloseWindow();
            }

            else if (Input.GetKeyDown(SingletonTable.singletonTable.kbM.curKeybindingSet[UserAction.Toggle_DetailedPlayerInfo_Window]))
            {
                if (SingletonTable.singletonTable.uwM.detailedPlayerInfoWindow.isActive == false) SingletonTable.singletonTable.uwM.detailedPlayerInfoWindow.OpenWindow();
                else SingletonTable.singletonTable.uwM.detailedPlayerInfoWindow.CloseWindow();
            }

        }
        else
        {
            //Need something for User : alert Sound...
        }
        
    }
}