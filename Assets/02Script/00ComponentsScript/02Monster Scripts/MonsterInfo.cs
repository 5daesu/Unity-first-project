using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    public int monsterCode;

    public bool physicalType;   //true = physical, false = magical
    public float maxHp;
    public float growHp;
    public float startDef;
    public float growDef;
    public float spd;

    public int cost;
    public int growCost;
}