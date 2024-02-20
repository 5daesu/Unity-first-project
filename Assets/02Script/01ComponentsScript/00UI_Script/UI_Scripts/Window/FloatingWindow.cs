using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingWindow : Window
{
    void Awake()
    {
        canvas = gameObject.GetComponent<Canvas>();
        isActive = false;
    }

    private void OnEnable()
    {
        isActive = true;
        ManagerGrouping.managerGrouping.uiM.MoveWindowToTop(this);
    }

    private void OnDisable()
    {
        isActive = false;
        ManagerGrouping.managerGrouping.uiM.CloseWindow(this);
    }
}
