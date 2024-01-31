using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class UnitData : ScriptableObject
{
    public string unitName;
    public int unitLevel;
    public int unitCode;
    public int MergeCode;   //(unitCode * 1000 ^ 2) + (unitCode * 1000 ^ 1) + (unitCode * 1000 ^ 0) is MergeCode
    public GameObject emptyTreeNode;
    public GameObject unitPrefab;
    public Sprite unitSprite;

    

    public GameObject MakeTreeNode(UnitData myself)
    {
        return Instantiate(emptyTreeNode);
    }

    /*
    public UnitData(string Name, int Code, GameObject Prefab, Sprite Image, List<int> Recipe, List<int> Number)   //constructor
    {
        unitName = Name;
        unitCode = Code;
        unitPrefab = Prefab;
        unitImage = Image;
        MergeRecipe = new int[Recipe.Count];
        MergeNumber = new int[Number.Count];
        MergeRecipe = Recipe.ToArray();
        MergeNumber = Number.ToArray();
    }
    */
}