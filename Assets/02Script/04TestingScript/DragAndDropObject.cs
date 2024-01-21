using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropObject : MonoBehaviour
{
    public bool mouseClick;

    void Awake()
    {
        mouseClick = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if()
    }
}
