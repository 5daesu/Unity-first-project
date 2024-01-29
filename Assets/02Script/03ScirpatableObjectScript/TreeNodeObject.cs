using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeNodeObject : MonoBehaviour
{
    public UnitData unitData;

    public Image unitImage;
    public Button treeNodeButton;

    void Start()
    {
    }

    public void OnClickTreeNode()
    {
        Debug.Log(unitData.unitName);
        if (unitData.unitLevel == 1) Debug.Log("Unit Level is 1, There's No Recipe");
        else Debug.Log("Unit Level is " + unitData.unitLevel);
    }
}
