using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitMergeRecipeList", menuName = "ScriptableObjects/UnitDataScriptableObjects/UnitMergeRecipeList", order = 4)]
public class UnitMergeRecipeList : ScriptableObject
{
    public List<UnitMergeRecipe> recipes = new List<UnitMergeRecipe>();

    public bool TryFindResult(IReadOnlyList<UnitData> materials, out UnitData result)
    {
        foreach (UnitMergeRecipe recipe in recipes)
        {
            if (recipe == null || recipe.Matches(materials) == false) continue;

            result = recipe.result;
            return result != null;
        }

        result = null;
        return false;
    }

    public List<UnitData> GetMaterialsForResult(UnitData result)
    {
        foreach (UnitMergeRecipe recipe in recipes)
        {
            if (recipe == null || recipe.HasResult(result) == false) continue;

            return new List<UnitData>(recipe.materials);
        }

        return new List<UnitData>();
    }

    public List<UnitData> GetResultsUsingMaterial(UnitData material)
    {
        List<UnitData> results = new List<UnitData>();

        foreach (UnitMergeRecipe recipe in recipes)
        {
            if (recipe == null || recipe.ContainsMaterial(material) == false || recipe.result == null) continue;

            results.Add(recipe.result);
        }

        return results;
    }
}
