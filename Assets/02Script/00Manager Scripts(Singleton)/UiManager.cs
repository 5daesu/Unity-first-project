using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public Window playerInfoBox;
    public Window stageInfoBox;
    public Window eventBox;
    public Window inventoryBox;

    public Window topWindow { get; set; }  //topWindow is lastNode of List

    //private Window[] windowList;
    private LinkedList<Window> windowList;
    private int windowCount;

    void Awake()
    {
        //windowList = new Window[4];
        windowList = new LinkedList<Window>();
        windowCount = 0;
    }

    public void MoveWindowToTop(Window targetWindow)    //When user click window or open new window
    {
        if (targetWindow.isActive == false)             //When window is opened newly
        {
            windowCount++;
            targetWindow.canvas.sortingOrder = windowCount;
            windowList.AddLast(targetWindow);
        }
        else                                            //When window is already opened
        {
            windowList.Remove(targetWindow);
            windowList.AddLast(targetWindow);

            RearrangeWindow();
        }
    }

    public void CloseWindow(Window targetWindow)    //
    {
        windowCount--;
        windowList.Remove(targetWindow);

        RearrangeWindow();
    }

    private void RearrangeWindow()    //Rearrange sortingorder
    {
        int i = 1;
        foreach (Window window in windowList)
        {
            window.canvas.sortingOrder = i;

            if (i == windowCount) topWindow = window;

            i++;
        }
    }
}