using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    public bool isCompleted;
    public UnitData[,] deckData;    //For save and load Deck

    private int[,] mergeCodeTable;
    private GameObject[,] unitPrefabTable;

    public void AssignMemory(UnitData blankUnitData)    //blankUnitData is for instead of "new"
    {
        deckData = new UnitData[4, 4];
        mergeCodeTable = new int[4, 4];
        unitPrefabTable = new GameObject[4, 4];

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                deckData[i, j] = blankUnitData;    //there's some caution in unity editor : i think because scriptableobject is designed to be made directly by asset in unity editor
                //mergeCodeTable[i, j] = new int();
                //unitPrefabTable[i, j] = new GameObject();
            }
        }
    }

    /*
    private void Awake()
    {
        deckData = new UnitData[4, 4];
        mergeCodeTable = new int[4, 4];
        unitPrefabTable = new GameObject[4, 4];

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                deckData[i, j] = new UnitData();
                mergeCodeTable[i, j] = new int();
                unitPrefabTable[i, j] = new GameObject();
            }
        }
    }
    */

    public void UpdateTable()
    {
        for (int i = 0; i < 4; i++) //i should be 4
        {
            for (int j = 0; j < 4; j++)
            {
                mergeCodeTable[i, j] = deckData[i, j].MergeCode;
                unitPrefabTable[i, j] = deckData[i, j].unitPrefab;
            }
        }
    }

    public GameObject Summon()
    {
        Debug.Log(unitPrefabTable[0, 0].name);
        return unitPrefabTable[0, Random.Range(0, 4)];
    }

    public GameObject Merge(int lv, int mergeCode)
    {
        GameObject mergedUnit = new GameObject();

        int i;
        if (lv == 1)
        {
            for (i = 0; i < 4; i++)
            {
                if (mergeCodeTable[0, i] == mergeCode)
                {
                    mergedUnit = unitPrefabTable[0, i];
                    break;
                }
            }
        }
        else if (lv == 2)
        {
            for (i = 0; i < 4; i++)
            {
                if (mergeCodeTable[1, i] == mergeCode)
                {
                    mergedUnit = unitPrefabTable[0, i];
                }
            }
        }
        else if (lv == 3)
        {
            for (i = 0; i < 4; i++)
            {
                if (mergeCodeTable[2, i] == mergeCode)
                {
                    mergedUnit = unitPrefabTable[0, i];
                }
            }
        }
        else if (lv == 4)
        {
            for (i = 0; i < 4; i++)
            {
                if (mergeCodeTable[3, i] == mergeCode)
                {
                    mergedUnit = unitPrefabTable[0, i];
                }
            }
        }

        return mergedUnit;
    }

}
