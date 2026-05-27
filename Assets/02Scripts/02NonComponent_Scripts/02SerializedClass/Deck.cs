using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    public bool isCompleted;
    public List<UnitData> deckPool { get; private set; }

    public Deck()
    {
        deckPool = new List<UnitData>();
    }

    public Deck(IEnumerable<UnitData> initialUnits)
    {
        deckPool = new List<UnitData>();
        AddUnits(initialUnits);
    }

    public void AddUnits(IEnumerable<UnitData> unitDataList)
    {
        if (unitDataList == null) return;

        foreach (UnitData unitData in unitDataList)
        {
            AddUnit(unitData);
        }
    }

    public void AddUnit(UnitData unitData)
    {
        if (unitData == null) return;
        if (deckPool == null) deckPool = new List<UnitData>();

        deckPool.Add(unitData);
    }

    public bool RemoveUnit(UnitData unitData)
    {
        if (deckPool == null || unitData == null) return false;

        return deckPool.Remove(unitData);
    }

    public UnitData SummonUnitData()
    {
        if (deckPool == null || deckPool.Count == 0)
        {
            Debug.LogWarning("Deck is empty. Can't summon unit.");
            return null;
        }

        List<UnitData> summonableUnits = new List<UnitData>();

        foreach (UnitData unitData in deckPool)
        {
            if (unitData == null || unitData.unitPrefab == null || unitData.unitLevel != 1) continue;

            summonableUnits.Add(unitData);
        }

        if (summonableUnits.Count == 0)
        {
            Debug.LogWarning("Deck has no level 1 unit to summon.");
            return null;
        }

        UnitData selectedUnitData = summonableUnits[Random.Range(0, summonableUnits.Count)];
        return selectedUnitData;
    }

    public GameObject Summon()
    {
        UnitData unitData = SummonUnitData();
        return unitData != null ? unitData.unitPrefab : null;
    }

    public UnitData Merge(UnitMergeRecipeList mergeRecipeList, params UnitData[] materials)
    {
        if (mergeRecipeList == null) return null;

        UnitData result;
        return mergeRecipeList.TryFindResult(materials, out result) ? result : null;
    }
}
