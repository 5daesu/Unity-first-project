using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeNodeGenerator : MonoBehaviour
{
    public TreeNodeDataList[] unitDataList;

    float Anchor_y_Value = 0.2f;     //0.2, 0.4, 0.6, 0.8
    float widthinterval = 40;
    float width;

    void Awake()
    {
        int listLength = 0;
        for (int i = 0; i < 4; i++)
        {
            if (unitDataList[i].unitDataList.Count > listLength) listLength = unitDataList[i].unitDataList.Count;
        }

        width = widthinterval * (2 * listLength + 1);
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 50);
    }

    void Start()
    {
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(45, 0, 0);

        int floor = 1;

        for(int i=0; i < 4; i++)    //i is Unit's Level
        {
            int index = 0;
            foreach (TreeNodeData treeNodeData in unitDataList[i].unitDataList)     //i is Unit's Level
            {
                GameObject treenode = treeNodeData.MakeTreeNode(treeNodeData);
                treenode.GetComponent<TreeNodeObject>().treeNodeData = treeNodeData;

                RectTransform rectTransform = treenode.GetComponent<RectTransform>();
                rectTransform.SetParent(gameObject.transform);                                                              //Set Parent
                rectTransform.transform.localPosition = new Vector3((-width * 0.5f) + (20 + 40 * (2 * index + 1)), 0, 0);   //Set Position
                rectTransform.sizeDelta = new Vector2(100, 100);                                                            //Set Size,     instead of treenode.GetComponent<RectTransform>().localScale = new Vector3(30, 30, 30);
                rectTransform.anchorMin = new Vector2(0.5f, Anchor_y_Value * floor);                                        //Set Anchor
                rectTransform.anchorMax = new Vector2(0.5f, Anchor_y_Value * floor);
                rectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);                                                   //Set Scale     I dont know why RectTransform's value is changed by itself

                index += 1;
            }

            floor += 1;
        }

    }
}
