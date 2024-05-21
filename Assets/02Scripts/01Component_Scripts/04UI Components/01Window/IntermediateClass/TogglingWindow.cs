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

    }

    protected virtual void OnDisable()
    {
        
    }

    public void OpenWindow()
    {
        togglingAction = GetComponent<TogglingAction>();
        if (togglingAction == null) togglingAction = gameObject.AddComponent<BasicTogglingAction>();

        gameObject.SetActive(true);
        ManagerGrouping.managerGrouping.uwM.SetWindowSortingOrderToTop(this);

        isActive = true;

        togglingAction.OpenAction();
    }

    public void CloseWindow()
    {
        isActive = false;
        ManagerGrouping.managerGrouping.uwM.RemoveWindowFromWindowList(this);

        togglingAction.CloseAction();
        //gameObject.SetActive(false);  //it must be in CloseAction
    }

    public override void UpdateWindowContent()
    {

    }
}