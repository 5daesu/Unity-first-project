using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public Window simplePlayerResourceWindow;
    public Window stageInfoWindow;
    public Window gameEventWindow;
    public Window inventoryWindow;

    public Window topWindow { get; set; }  //topWindow is lastNode of List

    //private Window[] windowList;
    private LinkedList<Window> windowList;

    void Awake()
    {
        //windowList = new Window[4];
        windowList = new LinkedList<Window>();
    }

    public void SetWindowSortingOrderToTop(Window targetWindow)    //When user click window or open new window
    {
        if (targetWindow.isActive == false)             //When window is opened newly
        {
            targetWindow.canvas.sortingOrder = windowList.Count;
            windowList.AddLast(targetWindow);
        }
        else                                            //When window is already opened
        {
            windowList.Remove(targetWindow);
            windowList.AddLast(targetWindow);

            ReorderWindowSortingOrder();
        }
    }

    public void RemoveWindowFromWindowList(Window targetWindow)     //Remove Window and Reorder Sorin
    {
        windowList.Remove(targetWindow);

        ReorderWindowSortingOrder();
    }

    private void ReorderWindowSortingOrder()    //Rearrange sortingorder
    {
        int i = 1;
        foreach (Window window in windowList)
        {
            window.canvas.sortingOrder = i;

            if (i == windowList.Count) topWindow = window;

            i++;
        }
    }
}