using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObjects/UnitDataScriptableObjects/SO_UnitData", order = 1)]
public class UnitData : ScriptableObject
{
    public string unitName;
    public int unitLevel;
    public int unitCode;
    public int mergeType;
    public int mergeCode;   //(unitCode * 1000 ^ 2) + (unitCode * 1000 ^ 1) + (unitCode * 1000 ^ 0) is MergeCode
    public GameObject emptyTreeNode;
    public GameObject unitPrefab;
    public Sprite unitSprite;
}