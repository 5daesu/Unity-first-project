using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TreeNodeData : ScriptableObject
{
    public string unitName;
    public int unitCode;
    public int unitLevel;
    public GameObject blankModel;
    public Sprite unitImage;

    public int[] MergeRecipe;  //int is for unitcode
    public int[] MergeNumber;

    public GameObject MakeTreeNode(TreeNodeData myself)
    {
        return Instantiate(blankModel);
    }

    /*
    public TreeNodeData(string Name, int Code, GameObject Prefab, Sprite Image, List<int> Recipe, List<int> Number)   //constructor
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