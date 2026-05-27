using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class KeyBindingManager : MonoBehaviour      //it is a set with KeyInputManager
{
    [SerializeField]
    public class KeyBindingPreset
    {
        public string[] keyCodeList;
    }

    //Definition of UserAction is in anotherScript but you can check that here.
    /*public enum UserAction
    {
        Move_RowUp,
        Move_RowDown,
        Move_ColumnUp,
        Move_ColumnDown,

        Press_MainButton,

        //UI
        Toggle_Inventory_Window,
        Toggle_Quest_Window,
        Toggle_DetailedPlayerInfo_Window
    }*/
    
    public Dictionary<UserAction, KeyCode> curKeybindingSet { get; private set; }

    string filePath = Application.dataPath;

    void Awake()
    {
        //if there is Local_File about Main InputBinding_Preset, Apply File to keybindingDict
        //else      //if there is No Main InputBinding_Preset in Local
        curKeybindingSet = new Dictionary<UserAction, KeyCode>();
        ResetAll();
    }

    //
    public void Bind(in UserAction action, in KeyCode code, bool allowOverlap = false)
    {
        if (!allowOverlap && curKeybindingSet.ContainsValue(code))
        {
            var copy = new Dictionary<UserAction, KeyCode>(curKeybindingSet);

            foreach (var pair in copy)
            {
                if (pair.Value.Equals(code))
                {
                    curKeybindingSet[pair.Key] = KeyCode.None;
                }
            }
        }
        curKeybindingSet[action] = code;
    }

    //Reset all Binding
    public void ResetAll()
    {
        Bind(UserAction.Move_RowUp, KeyCode.UpArrow);
        Bind(UserAction.Move_RowDown, KeyCode.DownArrow);
        Bind(UserAction.Move_ColumnUp, KeyCode.RightArrow);
        Bind(UserAction.Move_ColumnDown, KeyCode.LeftArrow);

        Bind(UserAction.Press_MainButton, KeyCode.Space);

        Bind(UserAction.Toggle_Inventory_Window, KeyCode.I);
        Bind(UserAction.Toggle_Quest_Window, KeyCode.J);
        Bind(UserAction.Toggle_DetailedPlayerInfo_Window, KeyCode.Tab);
    }

    public void SavePreset(int fileIndex)
    {
        KeyBindingPreset keyBindingPreset = new KeyBindingPreset();
        keyBindingPreset.keyCodeList = new string[System.Enum.GetValues(typeof(UserAction)).Length];

        int i = 0;
        foreach (UserAction userAction in System.Enum.GetValues(typeof(UserAction)))
        {
            keyBindingPreset.keyCodeList[i] = System.Enum.GetName(typeof(KeyCode), curKeybindingSet[userAction]);

            i++;
        }

        string jsonData = JsonUtility.ToJson(keyBindingPreset);
        File.WriteAllText(filePath + "/keyBindingPreset" + fileIndex, jsonData);
    }

    public void LoadPreset(int fileIndex)
    {
        KeyBindingPreset keyBindingPreset = new KeyBindingPreset();
        keyBindingPreset.keyCodeList = new string[System.Enum.GetValues(typeof(UserAction)).Length];

        string jsonData = File.ReadAllText(filePath + "/keyBindingPreset" + fileIndex);
        keyBindingPreset = JsonUtility.FromJson<KeyBindingPreset>(jsonData);

        int i = 0;
        foreach (UserAction userAction in System.Enum.GetValues(typeof(UserAction)))
        {
            curKeybindingSet[userAction] = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyBindingPreset.keyCodeList[i]);

            i++;
        }
    }
}
