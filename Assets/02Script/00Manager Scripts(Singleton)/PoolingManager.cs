using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public GameObject[] prefabs;

    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++) pools[index] = new List<GameObject>();

        Debug.Log(pools.Length);
    }

    public GameObject GetObject(int index)
    {
        GameObject select = null;

        foreach (GameObject find in pools[index])
        {
            if (!find.activeSelf)
            {
                select = find;
                select.SetActive(true);
                break;
            }
        }

        if(!select)
        {
            //select = Instantiate(prefabs[index], Vector3 ~, parentObject.transform, parentObject.transform);
            select = Instantiate(prefabs[index], gameObject.transform);
            pools[index].Add(select);
        }

        return select;
    }

    public GameObject GetObject(int index, GameObject parentGameObject)     //function overloading
    {
        GameObject select = null;

        foreach (GameObject find in pools[index])
        {
            if (!find.activeSelf)
            {
                select = find;
                select.transform.parent = parentGameObject.transform;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(prefabs[index], parentGameObject.transform);
            pools[index].Add(select);
        }

        return select;
    }
}
