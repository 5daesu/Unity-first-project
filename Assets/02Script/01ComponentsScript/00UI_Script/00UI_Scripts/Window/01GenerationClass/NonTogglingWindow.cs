using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonTogglingWindow : Window
{
    void Awake()
    {
        canvas = gameObject.GetComponent<Canvas>();
        isActive = true;
    }

    void Start()
    {
        ManagerGrouping.managerGrouping.uiM.SetWindowSortingOrderToTop(this);
    }

    public override void UpdateWindowContent()
    {

    }
}
