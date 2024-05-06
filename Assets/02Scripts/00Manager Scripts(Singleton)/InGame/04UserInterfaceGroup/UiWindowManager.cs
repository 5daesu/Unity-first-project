using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiWindowManager : MonoBehaviour
{
    //0.GameMenu Window Objects
    //public Window deckBuildingWindow;

    //1.InGame Window
    //1-1. InGame NonTogglingWindow
    public NonTogglingWindow simplePlayerResourceWindow;
    public NonTogglingWindow stageInfoWindow;
    //1-2. InGame TogglingWindow
    public TogglingWindow gameEventWindow;
    public TogglingWindow inventoryWindow;
    public TogglingWindow questWindow;
    public TogglingWindow detailedPlayerInfoWindow;

    //2.Common Window Objects


    public TogglingWindow topTogglingWindow { get; set; }  //topWindow is lastNode of List

    //private Window[] togglingWindows;
    //private List<NonTogglingWindow> nonTogglingWindows;
    private List<TogglingWindow> togglingWindows; 

    void Awake()
    {
        //togglingWindows = new Window[4];
        //nonTogglingWindows = new List<NonTogglingWindow>();
        togglingWindows = new List<TogglingWindow>();
    }

    public void InitializeWindowSortingOrder(NonTogglingWindow targetWindow)    //Only in NonTogglingWindow
    {
        targetWindow.canvas.sortingOrder = 0;   //Because NonTogglingWindows can't overlap eachothers
        targetWindow.isActive = true;
    }

    public void SetWindowSortingOrderToTop(TogglingWindow targetWindow)    //When user click window or open new window
    {
        if (targetWindow.isActive == false)             //When window is opened newly
        {
            targetWindow.canvas.sortingOrder = togglingWindows.Count;
            togglingWindows.Add(targetWindow);
        }
        else                                            //When window is already opened, Forexample when user click window, it should go to top
        {
            togglingWindows.Remove(targetWindow);
            togglingWindows.Add(targetWindow);

            ReorderWindowSortingOrder();
        }
    }

    public void RemoveWindowFromWindowList(TogglingWindow targetWindow)     //Remove Window and Reorder Sorin
    {
        togglingWindows.Remove(targetWindow);

        ReorderWindowSortingOrder();
    }

    private void ReorderWindowSortingOrder()    //Rearrange sortingorder
    {
        int i = 1;
        foreach (TogglingWindow window in togglingWindows)
        {
            window.canvas.sortingOrder = i;

            if (i == togglingWindows.Count) topTogglingWindow = window;

            i++;
        }
    }
}