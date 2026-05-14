using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEventWindow : TogglingWindow
{
    [SerializeField] private Image gameEventImage;
    [SerializeField] private Text gameEventName;
    [SerializeField] private Text gameEventBody;
    [SerializeField] private Button[] buttons;
    [SerializeField] private Text[] texts;

    protected override void OnEnable()
    {
        base.OnEnable();

        UpdateWindowContent(SingletonTable.singletonTable.geM.curStageGameEvent);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    public void UpdateWindowContent(GameEvent gameEvent)
    {
        gameEventImage.sprite = gameEvent.gameEventSprite;
        gameEventName.text = gameEvent.gameEventName;
        gameEventBody.text = gameEvent.gameEventBodyText;

        ResetWindowButton(gameEvent);
    }

    private void ResetWindowButton(GameEvent gameEvent)
    {
        if(gameEvent is NoticeGameEvent)
        {
            buttons[0].interactable = true;
            buttons[1].gameObject.SetActive(false);
            buttons[2].gameObject.SetActive(false);
            buttons[3].gameObject.SetActive(false);
        }
        else
        {
        }
    }

    public void OnClickButton0()
    {
        Debug.Log("Clicked");

        if (SingletonTable.singletonTable.geM.curStageGameEvent is NoticeGameEvent)
        {
            SingletonTable.singletonTable.geM.curStageGameEvent.StartGameEvent();
            CloseWindow();
        }
        //else curGameEvent.StartGameEvent(0);
    }

    public void OnClickButton1()
    {
        //else curGameEvent.StartGameEvent(1);
    }

    public void OnClickButton2()
    {
        //else curGameEvent.StartGameEvent(2);
    }

    public void OnClickButton3()
    {
        //else curGameEvent.StartGameEvent(3);
    }
}