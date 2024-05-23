using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TogglingWindow : Window, IPointerDownHandler
{
    private TogglingAction togglingAction;

    [SerializeField] private bool dragOption;                   //if value is true : window can be draged
    [SerializeField] private bool timeStopOption;               //if value is true : when window is on activated, time is stpo (by Time.timescale = 0)
    [SerializeField] private bool inputDisabledOption;          //if value is true : 
    [SerializeField] private bool fixedCloseMethodOption;       //if value is true : the window should be closed by fixed Method (it will be controlled by closeCondition)

    private bool closeCondition = true;  //For fixedClosingMethod

    protected override void Awake()
    {
        base.Awake();

        if ((dragOption == true) && !(GetComponent<DragAction>())) gameObject.AddComponent<DragAction>();   //About DragOption
    }

    protected virtual void OnEnable()
    {
        if (timeStopOption) Time.timeScale = 0;
        if (inputDisabledOption) ManagerGrouping.managerGrouping.kiM.InputDisabledWindowStackUp();
        if (fixedCloseMethodOption) closeCondition = false;     //In the Window, User must be able to change the value
    }

    protected virtual void OnDisable()
    {
        if (timeStopOption) Time.timeScale = 1;
        if (inputDisabledOption) ManagerGrouping.managerGrouping.kiM.InputDisabledWindowStackDown();
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
        if (!closeCondition) return;
        else
        {
            isActive = false;
            ManagerGrouping.managerGrouping.uwM.RemoveWindowFromWindowList(this);

            togglingAction.CloseAction();
            //gameObject.SetActive(false);  //it must be in CloseAction
        }
    }

    public void OnPointerDown(PointerEventData eventData)   //If a Window is Clicked, it should go to top
    {
        ManagerGrouping.managerGrouping.uwM.SetWindowSortingOrderToTop(this);
    }

    public override void UpdateWindowContent()
    {

    }
}