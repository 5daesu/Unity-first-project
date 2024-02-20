using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedWindow : Window
{
    void Awake()
    {
        canvas = gameObject.GetComponent<Canvas>();
        isActive = true;
    }

    void Start()
    {
        ManagerGrouping.managerGrouping.uiM.MoveWindowToTop(this);
    }
}
