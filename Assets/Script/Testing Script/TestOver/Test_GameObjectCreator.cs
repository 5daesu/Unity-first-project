using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectCreator : MonoBehaviour
{
    void Start()
    {
        for (int i = 1; i < 2; i++)
        {
            for (int j = 1; j < 2; j++)
            {
                GameObject newObject = new GameObject();
                //newObject.AddComponent<Transform>();
                newObject.transform.position = new Vector3(-3 + i, -3 + j, 0);
                //newObject.AddComponent<SpriteRenderer>();
                //newObject.AddComponent<Renderer>();
                //newObject.rendere
                Debug.Log("ss");
            }
        }
    }

    void Update()
    {

    }
}