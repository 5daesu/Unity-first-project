using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingWindow : MonoBehaviour
{
    [SerializeField] GameObject soundSettingTab;
    [SerializeField] GameObject keyBindingTab;

    private GameObject curTab;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Event Method About Click a Button
    //There's 4 kind of Button in this Window
    //1. 2. 3. 4.
    public void OnClickSoundSettingButton()
    {
        ChangeTab(soundSettingTab);
    }

    public void OnClickKeyBindingButton()
    {
        ChangeTab(keyBindingTab);
    }

    public void OnClickSaveButton()
    {
        SingletonTable.singletonTable.kbM.SavePreset(0);
        Debug.Log("Save Button Clicked");
    }

    private void ChangeTab(GameObject newTab)
    {
        curTab.SetActive(false);
        newTab.SetActive(true);
    }
}
