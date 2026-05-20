using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitMergeRecipe", menuName = "ScriptableObjects/UnitDataScriptableObjects/UnitMergeRecipe", order = 3)]
public class UnitMergeRecipe : ScriptableObject
{
    public List<UnitData> materials = new List<UnitData>();
    public UnitData result;

    public bool HasResult(UnitData unitData)
    {
        return result == unitData;
    }

    public bool ContainsMaterial(UnitData unitData)
    {
        return materials.Contains(unitData);
    }

    public bool Matches(IReadOnlyList<UnitData> targetMaterials)
    {
        if (targetMaterials == null || materials.Count != targetMaterials.Count) return false;

        bool[] used = new bool[targetMaterials.Count];

        foreach (UnitData material in materials)
        {
            bool found = false;

            for (int i = 0; i < targetMaterials.Count; i++)
            {
                if (used[i] || targetMaterials[i] != material) continue;

                used[i] = true;
                found = true;
                break;
            }

            if (found == false) return false;
        }

        return true;
    }
}
