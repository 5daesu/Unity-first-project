using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMove : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
