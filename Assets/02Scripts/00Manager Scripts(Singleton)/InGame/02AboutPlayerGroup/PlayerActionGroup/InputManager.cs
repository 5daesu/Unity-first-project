using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class InputManager : MonoBehaviour   //Manage Input and KeyBinding
{
    public enum UserAction
    {
        MoveRowUp,
        MoveRowDown,
        MoveColumnUp,
        MoveColumnDown,

        MainButton,

        //UI
        Open_GameSetting_Window,
        Open_Inventory_Window
    }

    private Dictionary<UserAction, KeyCode> keyBindingDict;

    public class InputBinding
    {
        public Dictionary<UserAction, KeyCode> keyBindingDict { get; set; }

        public InputBinding(bool initialize = true)
        {
            keyBindingDict = new Dictionary<UserAction, KeyCode>();

            if(initialize)
            {
                //ResetAll();
            }
        }

        public void ApplyNewBindings(InputBinding newBinding)
        {
            keyBindingDict = new Dictionary<UserAction, KeyCode>(newBinding.keyBindingDict);
        }

        public void Bind(in UserAction action, in KeyCode code, bool allowOverlap = false)
        {
            if(!allowOverlap)
        }
    }
}
*/