using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeNodeGenerator : MonoBehaviour
{
    public List<TreeNodeData> lv1_UnitList;
    public List<TreeNodeData> lv2_UnitList;
    public List<TreeNodeData> lv3_UnitList;
    public List<TreeNodeData> lv4_UnitList;

    float Anchor_y_Value = 0.2f;     //0.2, 0.4, 0.6, 0.8
    float widthInterval;

    void Awake()
    {
        
    }

    void Start()
    {
        foreach(TreeNodeData treeNodeData in lv1_UnitList)
        {
            GameObject treenode = treeNodeData.MakeTreeNode(treeNodeData);
            treenode.GetComponent<RectTransform>().SetParent(gameObject.transform);
            treenode.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, Anchor_y_Value);
            treenode.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, Anchor_y_Value);
            Anchor_y_Value += Anchor_y_Value;
        }
    }
}
