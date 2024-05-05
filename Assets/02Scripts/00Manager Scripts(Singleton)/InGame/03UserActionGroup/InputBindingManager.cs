using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBindingManager : MonoBehaviour
{
    public Dictionary<UserAction, KeyCode> inputbindingDict { get; private set; }

    void Awake()
    {
        //if there is Local_File about Main InputBinding_Preset, Apply File to inputbindingDict
        //else      //if there is No Main InputBinding_Preset in Local
        inputbindingDict = new Dictionary<UserAction, KeyCode>();
        ResetAll();
    }

    //
    public void Bind(in UserAction action, in KeyCode code, bool allowOverlap = false)
    {
        if (!allowOverlap && inputbindingDict.ContainsValue(code))
        {
            var copy = new Dictionary<UserAction, KeyCode>(inputbindingDict);

            foreach (var pair in copy)
            {
                if (pair.Value.Equals(code))
                {
                    inputbindingDict[pair.Key] = KeyCode.None;
                }
            }
        }
        inputbindingDict[action] = code;
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

    // ������
    public InputBinding(bool initalize = true)
    {
        _bindingDict = new Dictionary<UserAction, KeyCode>();

        if (initalize)
        {
            ResetAll();
        }
    }

    // ���ο� ���ε� ����
    public void ApplyNewBindings(InputBinding newBinding)
    {
        _bindingDict = new Dictionary<UserAction, KeyCode>(newBinding._bindingDict);
    }

    // ���ε� ���� �޼ҵ� : allowOverlap �Ű������� ���� �ߺ� ���ε� ��뿩�θ� �����Ѵ�.
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

    // �ʱ� ���ε��� ���� �޼ҵ�
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