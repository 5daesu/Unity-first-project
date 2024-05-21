using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingScreen : MonoBehaviour
{
    //In the GameSettingScreen There's a some UI objects;
    [SerializeField] TogglingWindow gameSettingWindow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Event Method About Click a Button
    //There's 3 kind of Button in this Window
    //1. 2. 3.
    public void OnClickResumeButton()
    {
        if (gameSettingWindow.isActive == true) gameSettingWindow.CloseWindow();
        ManagerGrouping.managerGrouping.uwM.gameSettingScreen.CloseWindow();
    }

    public void OnClickGameSettingButton()
    {
        gameSettingWindow.OpenWindow();
    }

    public void OnClickExitButton()
    {

    }
}
