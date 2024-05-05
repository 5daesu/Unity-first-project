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
        ManagerGrouping.managerGrouping.uwM.SetWindowSortingOrderToTop(this);
    }

    protected virtual void OnDisable()
    {
        isActive = false;
        ManagerGrouping.managerGrouping.uwM.RemoveWindowFromWindowList(this);
    }

    public void OpenWindow()
    {
        isActive = true;

        togglingAction = GetComponent<TogglingAction>();
        if (togglingAction == null) togglingAction = gameObject.AddComponent<BasicTogglingAction>();

        Debug.Log("Open Window");
        gameObject.SetActive(true);
        togglingAction.OpenAction();
    }

    public void CloseWindow()
    {
        isActive = false;

        togglingAction.CloseAction();
        //gameObject.SetActive(false);  //it must be in CloseAction
    }

    public override void UpdateWindowContent()
    {

    }
}