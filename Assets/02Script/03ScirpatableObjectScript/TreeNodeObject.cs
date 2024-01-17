using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeNodeObject : MonoBehaviour
{
    public TreeNodeData treeNodeData;
    public Button treeNodeButton;

    void Start()
    {
    }

    public void OnClickTreeNode()
    {
        Debug.Log(treeNodeData.unitName);
        if (treeNodeData.unitLevel == 1) Debug.Log("Unit Level is 1, There's No Recipe");
        else Debug.Log("Unit Level is " + treeNodeData.unitLevel);
    }
}
