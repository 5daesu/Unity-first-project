using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEventWindow : TogglingWindow
{
    [SerializeField] private Image gameEventImage;
    [SerializeField] private TMP_Text gameEventNameTMP;
    [SerializeField] private TMP_Text gameEventBodyTMP;
    [SerializeField] private TMP_Text[] tmpTexts;
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
        if (gameEvent == null)
        {
            SetAllButtonsInactive();
            return;
        }

        gameEventImage.sprite = gameEvent.gameEventSprite;
        SetText(gameEventNameTMP, gameEventName, gameEvent.gameEventName);
        SetText(gameEventBodyTMP, gameEventBody, gameEvent.gameEventBodyText);

        ResetWindowButton(gameEvent);
    }

    private void ResetWindowButton(GameEvent gameEvent)
    {
        if (gameEvent == null)
        {
            SetAllButtonsInactive();
            return;
        }

        SetAllButtonsInactive();

        ChoiceNoticeGameEvent choiceEvent = gameEvent as ChoiceNoticeGameEvent;

        if (choiceEvent != null && choiceEvent.HasChoices())
        {
            if (buttons == null) return;

            int choiceCount = Mathf.Min(choiceEvent.ChoiceCount, buttons.Length);

            for (int i = 0; i < choiceCount; i++)
            {
                if (buttons[i] == null) continue;

                buttons[i].gameObject.SetActive(true);
                buttons[i].interactable = true;
                SetButtonText(i, choiceEvent.GetChoiceText(i));
            }

            return;
        }

        if (gameEvent is NoticeGameEvent)
        {
            if (buttons == null || buttons.Length == 0 || buttons[0] == null) return;

            buttons[0].gameObject.SetActive(true);
            buttons[0].interactable = true;
            SetButtonText(0, "\uD655\uC778");
        }
    }

    private void SetText(TMP_Text tmpText, Text legacyText, string value)
    {
        if (tmpText != null)
        {
            tmpText.gameObject.SetActive(true);
            tmpText.text = value;
            if (legacyText != null) legacyText.enabled = false;
            return;
        }

        if (legacyText == null) return;

        legacyText.gameObject.SetActive(true);
        legacyText.enabled = true;
        legacyText.text = value;
    }

    private void SetButtonText(int buttonIndex, string value)
    {
        TMP_Text tmpText = GetButtonTMPText(buttonIndex);
        Text legacyText = GetButtonLegacyText(buttonIndex);

        if (tmpText != null)
        {
            tmpText.gameObject.SetActive(true);
            tmpText.text = value;
            tmpText.enableWordWrapping = true;
            tmpText.overflowMode = TextOverflowModes.Overflow;
            tmpText.alignment = TextAlignmentOptions.Center;

            if (legacyText != null) legacyText.enabled = false;
            return;
        }

        if (legacyText == null) return;

        legacyText.gameObject.SetActive(true);
        legacyText.enabled = true;
        legacyText.text = value;
        legacyText.alignment = TextAnchor.MiddleCenter;
        legacyText.horizontalOverflow = HorizontalWrapMode.Wrap;
        legacyText.verticalOverflow = VerticalWrapMode.Overflow;
        legacyText.resizeTextForBestFit = true;
        legacyText.resizeTextMinSize = 8;
        legacyText.resizeTextMaxSize = 14;
    }

    private TMP_Text GetButtonTMPText(int buttonIndex)
    {
        if (tmpTexts != null && buttonIndex >= 0 && buttonIndex < tmpTexts.Length && tmpTexts[buttonIndex] != null)
        {
            return tmpTexts[buttonIndex];
        }

        if (buttons == null || buttonIndex < 0 || buttonIndex >= buttons.Length || buttons[buttonIndex] == null) return null;

        return buttons[buttonIndex].GetComponentInChildren<TMP_Text>(true);
    }

    private Text GetButtonLegacyText(int buttonIndex)
    {
        if (texts != null && buttonIndex >= 0 && buttonIndex < texts.Length && texts[buttonIndex] != null)
        {
            return texts[buttonIndex];
        }

        if (buttons == null || buttonIndex < 0 || buttonIndex >= buttons.Length || buttons[buttonIndex] == null) return null;

        return buttons[buttonIndex].GetComponentInChildren<Text>(true);
    }

    private void SetAllButtonsInactive()
    {
        if (buttons == null) return;

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] == null) continue;

            buttons[i].interactable = false;
            buttons[i].gameObject.SetActive(false);
        }
    }

    public void OnClickButton0()
    {
        ExecuteCurrentGameEvent(0);
        CloseWindow();
    }

    public void OnClickButton1()
    {
        ExecuteCurrentGameEvent(1);
        CloseWindow();
    }

    public void OnClickButton2()
    {
        ExecuteCurrentGameEvent(2);
        CloseWindow();
    }

    public void OnClickButton3()
    {
        ExecuteCurrentGameEvent(3);
        CloseWindow();
    }

    private void ExecuteCurrentGameEvent(int choiceIndex)
    {
        GameEvent curGameEvent = SingletonTable.singletonTable.geM.curStageGameEvent;
        if (curGameEvent == null) return;

        if (curGameEvent is ChoiceNoticeGameEvent choiceEvent) choiceEvent.StartGameEvent(choiceIndex);
        else curGameEvent.StartGameEvent();
    }
}
