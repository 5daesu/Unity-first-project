using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonTogglingWindow : Window
{
    void OnEnable()
    {
        SingletonTable.singletonTable.uwM.InitializeWindowSortingOrder(this);
    }

    public override void UpdateWindowContent()
    {

    }
}
