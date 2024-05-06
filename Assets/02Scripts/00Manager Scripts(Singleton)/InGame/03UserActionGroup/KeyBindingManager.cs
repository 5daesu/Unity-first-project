using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindingManager : MonoBehaviour      //it is a set with KeyInputManager
{
    public Dictionary<UserAction, KeyCode> keybindingDict { get; private set; }

    void Awake()
    {
        //if there is Local_File about Main InputBinding_Preset, Apply File to keybindingDict
        //else      //if there is No Main InputBinding_Preset in Local
        keybindingDict = new Dictionary<UserAction, KeyCode>();
        ResetAll();
    }

    //
    public void Bind(in UserAction action, in KeyCode code, bool allowOverlap = false)
    {
        if (!allowOverlap && keybindingDict.ContainsValue(code))
        {
            var copy = new Dictionary<UserAction, KeyCode>(keybindingDict);

            foreach (var pair in copy)
            {
                if (pair.Value.Equals(code))
                {
                    keybindingDict[pair.Key] = KeyCode.None;
                }
            }
        }
        keybindingDict[action] = code;
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
}

/*
[SerializeField]
public class InputBinding
{
    public Dictionary<UserAction, KeyCode> Bindings => _bindingDict;
    private Dictionary<UserAction, KeyCode> _bindingDict;

    // 생성자
    public InputBinding(bool initalize = true)
    {
        _bindingDict = new Dictionary<UserAction, KeyCode>();

        if (initalize)
        {
            ResetAll();
        }
    }

    // 새로운 바인딩 적용
    public void ApplyNewBindings(InputBinding newBinding)
    {
        _bindingDict = new Dictionary<UserAction, KeyCode>(newBinding._bindingDict);
    }

    // 바인딩 지정 메소드 : allowOverlap 매개변수를 통해 중복 바인딩 허용여부를 결정한다.
    public void Bind(in UserAction action, in KeyCode code, bool allowOverlap = false)
    {
        if (!allowOverlap && _bindingDict.ContainsValue(code))
        {
            var copy = new Dictionary<UserAction, KeyCode>(_bindingDict);

            foreach (var pair in copy)
            {
                if (pair.Value.Equals(code))
                {
                    _bindingDict[pair.Key] = KeyCode.None;
                }
            }
        }
        _bindingDict[action] = code;
    }

    // 초기 바인딩셋 지정 메소드
    public void ResetAll()
    {
        Bind(UserAction.Move_RowUp, KeyCode.UpArrow);
        Bind(UserAction.Move_RowDown, KeyCode.DownArrow);
        Bind(UserAction.Move_ColumnUp, KeyCode.RightArrow);
        Bind(UserAction.Move_ColumnDown, KeyCode.LeftArrow);

        Bind(UserAction.Press_MainButton, KeyCode.LeftArrow);

        Bind(UserAction.Open_Inventory_Window, KeyCode.LeftArrow);
    }
}
*/