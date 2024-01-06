using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeperatedPool : MonoBehaviour
{
    public GameObject[] prefabs;

    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++) pools[index] = new List<GameObject>();

        Debug.Log(pools.Length);
    }
}
