using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglingWindow : Window
{
    private TogglingAction togglingAction;

    void Awake()
    {
        canvas = gameObject.GetComponent<Canvas>();
        isActive = false;
    }

    protected virtual void OnEnable()
    {
        isActive = true;
        ManagerGrouping.managerGrouping.uiM.SetWindowSortingOrderToTop(this);
    }

    protected virtual void OnDisable()
    {
        isActive = false;
        ManagerGrouping.managerGrouping.uiM.RemoveWindowFromWindowList(this);
    }

    public void OpenWindow()
    {
        togglingAction = GetComponent<TogglingAction>();
        if (togglingAction == null) togglingAction = gameObject.AddComponent<BasicAction>();

        Debug.Log("Open Window");
        togglingAction.OpenAction();
    }

    public void CloseWindow()
    {
        togglingAction.CloseAction();
    }

    public override void UpdateWindowContent()
    {

    }
}