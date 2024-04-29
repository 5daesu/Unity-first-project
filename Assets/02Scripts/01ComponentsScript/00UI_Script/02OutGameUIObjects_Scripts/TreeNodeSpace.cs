using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeNodeSpace : MonoBehaviour  //TreeNodeGenerator also make sapce for treeNode
{
    public UnitInfoBox unitInfoBox;

    [SerializeField] private GameObject blankTreeNode;
    [SerializeField] private UnitDataList[] unitDataList;

    private GameObject[,] treeNodeArray;

    private float anchor_y_Value = 0.2f;     //0.2, 0.4, 0.6, 0.8
    private float widthinterval = 40;
    private float width;

    private int listLength = 0;
    private int[] lvLength;

    private List<GameObject> selectedTreeNodes;

    void Awake()
    {
        lvLength = new int[4];

        for (int i = 0; i < 4; i++)
        {
            lvLength[i] = unitDataList[i].unitDataList.Count;

            if (unitDataList[i].unitDataList.Count > listLength) listLength = unitDataList[i].unitDataList.Count;   //Update length by list.count and it will be used to make space for scroll view's inside space
        }
        
        width = widthinterval * (2 * listLength + 1);
        GetComponent<RectTransform>().sizeDelta = new Vector2(width, 50);

        treeNodeArray = new GameObject[4, listLength];
    }

    void Start()
    {
        GenerateTreeNode();
    }

    private void GenerateTreeNode()
    {
        GetComponent<RectTransform>().localPosition = new Vector3(45, 0, 0);

        for (int i = 0; i < 4; i++)    //i is Unit's Level
        {
            int index = 0;
            foreach (UnitData unitData in unitDataList[i].unitDataList)     //index is Unit's order in same level
            {
                GameObject treeNode = Instantiate(blankTreeNode);
                FillTreeNodeData(treeNode.GetComponent<UnitDataTreeNode>(), unitData);
                PlaceTreeNode(i + 1, index, treeNode.GetComponent<RectTransform>());
                treeNodeArray[i, index] = treeNode;

                index += 1;
            }
        }
    }

    private void FillTreeNodeData(UnitDataTreeNode unitDataTreeNode, UnitData unitData)
    {
        unitDataTreeNode.unitData = unitData;
        unitDataTreeNode.unitCode = unitData.unitCode;
        unitDataTreeNode.ResetUnitImage(unitData.unitPrefab.GetComponent<SpriteRenderer>().sprite);
    }

    private void PlaceTreeNode(int floor, int index, RectTransform treeNodeRectTransform)
    {
        treeNodeRectTransform.SetParent(gameObject.transform);                                                              //Set Parent
        treeNodeRectTransform.transform.localPosition = new Vector3((-width * 0.5f) + (20 + 40 * (2 * index + 1)), 0, 0);   //Set Position
        treeNodeRectTransform.sizeDelta = new Vector2(100, 100);                                                            //Set Size,     instead of treenode.GetComponent<RectTransform>().localScale = new Vector3(30, 30, 30);
        treeNodeRectTransform.anchorMin = new Vector2(0.5f, anchor_y_Value * floor);                                        //Set Anchor
        treeNodeRectTransform.anchorMax = new Vector2(0.5f, anchor_y_Value * floor);
        treeNodeRectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);                                                   //Set Scale     I dont know why RectTransform's value is changed by itself
    }

    private GameObject FindTreeNode(int unitCode)
    {
        int unitLevel = unitCode / 100;

        for (int i = 0; i < lvLength[unitLevel - 1]; i++)
        {
            Debug.Log("Find TreeNode");
            if (unitCode == treeNodeArray[unitLevel - 1, i].GetComponent<UnitDataTreeNode>().unitCode) return treeNodeArray[unitLevel - 1, i];
        }

        Debug.Log("Can't Find TreeNode");
        return null;
    }

    private void FindChildTreeNode(int unitCode)
    {
        UnitData unitData = FindTreeNode(unitCode).GetComponent<UnitDataTreeNode>().unitData;

        int level = unitData.unitLevel;
        int mType = unitData.mergeType;
        int mCode = unitData.mergeCode;

        if (level == 1) return; //there's no ChildTreeNode
        else
        {
            List<int> unitCodeList = DissolveMergeCode(mCode, mType);

            foreach(int uCode in unitCodeList)
            {
                FindTreeNode(uCode).GetComponent<UnitDataTreeNode>().MarkTreeNode();
                FindChildTreeNode(uCode);
            }
        }
    }

    public void FindChildTreeNode(UnitData unitData)
    {
        int level = unitData.unitLevel;
        int mType = unitData.mergeType;
        int mCode = unitData.mergeCode;

        if (level == 1) return; //there's no ChildTreeNode
        else
        {
            List<int> unitCodeList = DissolveMergeCode(mCode, mType);

            foreach (int uCode in unitCodeList)
            {
                FindTreeNode(uCode).GetComponent<UnitDataTreeNode>().MarkTreeNode();
                FindChildTreeNode(uCode);
            }
        }
    }

    private List<int> DissolveMergeCode(int mergeCode, int mergeType)
    {
        List<int> unitCodeList = new List<int>();
        int extractedCode;

        for (int i = 0; i < mergeType; i++)
        {
            extractedCode = mergeCode / Power_Of_N(1000, (mergeType - 1 - i));
            mergeCode -= extractedCode * Power_Of_N(1000, (mergeType - 1 - i));
            unitCodeList.Add(extractedCode);
        }

        return unitCodeList;
    }

    private int Power_Of_N(int underNumber, int n)
    {
        if (n == 0) return 1;
        else if (n == 1) return underNumber;
        else
        {
            for (int i = 0; i < n - 1; i++) underNumber *= underNumber;
            return underNumber;
        }
    }

    private void FindParentTreeNode(int unitCode)
    {
        int level = unitCode / 100;

        if (level == 4) return; //there's no ParentTreeNode
        else
        {
            UnitData uData;
            for (int i = 0; i < lvLength[level]; i++)
            {
                uData = treeNodeArray[level, i].GetComponent<UnitDataTreeNode>().unitData;

                foreach (int uCode in DissolveMergeCode(uData.mergeCode, uData.mergeType))
                {
                    if (unitCode == uCode) treeNodeArray[level, i].GetComponent<UnitDataTreeNode>().MarkTreeNode();
                }
            }
        }
    }

    public void FindParentTreeNode(UnitData unitData)
    {
        int unitCode = unitData.unitCode;
        int level = unitData.unitLevel;

        if (level == 4) return; //there's no ParentTreeNode
        else
        {
            UnitData uData;
            for (int i = 0; i < lvLength[level]; i++)
            {
                uData = treeNodeArray[level, i].GetComponent<UnitDataTreeNode>().unitData;

                foreach (int uCode in DissolveMergeCode(uData.mergeCode, uData.mergeType))
                {
                    if (unitCode == uCode) treeNodeArray[level, i].GetComponent<UnitDataTreeNode>().MarkTreeNode();
                }
            }
        }
    }

    public void HighLightTreeNode(GameObject treeNode)
    {

    }

    public void UnSelectTreeNode(GameObject treeNode)
    {

    }
}