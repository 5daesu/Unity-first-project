using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager1 : MonoBehaviour
{
    /*
    public List<PoolData> prefabs;

    List <List<GameObject>> pools;

    
    void Awake()
    {
        pools = new List<List<GameObject>>();
    }
    
    //public bool CheckPool(GameObject targetObject)      //check gameObject in List
    //{
    //    bool haveTarget = false;
    //    foreach (GameObject find in prefabs)
    //    {
    //        if (find == targetObject) haveTarget = true;
    //    }
    //    return haveTarget;
    //}
    
    public GameObject GetObject(int index, GameObject parentUnit)
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
            select = Instantiate(prefabs[index], parentUnit.transform);
            pools[index].Add(select);
        }

        return select;
    }
    */
}
