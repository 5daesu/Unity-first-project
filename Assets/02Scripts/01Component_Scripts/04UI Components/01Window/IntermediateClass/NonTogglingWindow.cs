using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonTogglingWindow : Window
{
    void OnEnable()
    {
        ManagerGrouping.managerGrouping.uwM.InitializeWindowSortingOrder(this);
    }

    public override void UpdateWindowContent()
    {

    }
}
