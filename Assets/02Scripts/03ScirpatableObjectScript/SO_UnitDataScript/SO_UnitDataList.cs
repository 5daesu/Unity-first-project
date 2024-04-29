using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitDataList", menuName = "ScriptableObjects/UnitDataScriptableObjects/SO_UnitDataList", order = 2)]
public class UnitDataList : ScriptableObject
{
    public List<UnitData> unitDataList;
}