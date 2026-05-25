using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChoiceNoticeGameEvent", menuName = "ScriptableObjects/GameEvents/ChoiceNoticeGameEvent", order = 1)]
public class ChoiceNoticeGameEvent : NoticeGameEvent
{
    [SerializeField] private GameEventChoice[] choices;

    public bool HasChoices()
    {
        return choices != null && choices.Length > 0;
    }

    public int ChoiceCount
    {
        get
        {
            if (choices == null) return 0;

            return choices.Length;
        }
    }

    public string GetChoiceText(int choiceIndex)
    {
        if (choices == null || choiceIndex < 0 || choiceIndex >= choices.Length) return string.Empty;

        return choices[choiceIndex].GetDisplayText();
    }

    public void StartGameEvent(int choiceIndex)
    {
        if (choices == null || choiceIndex < 0 || choiceIndex >= choices.Length) return;

        choices[choiceIndex].Execute();
    }
}

[System.Serializable]
public class GameEventChoice
{
    [SerializeField] private string choiceText;
    [SerializeField] private string resultPreviewText;
    [SerializeField] private GameEventAction[] actions;

    public string GetDisplayText()
    {
        if (string.IsNullOrEmpty(resultPreviewText)) return choiceText;

        return choiceText + "\n" + resultPreviewText;
    }

    public void Execute()
    {
        if (actions == null) return;

        foreach (GameEventAction action in actions)
        {
            if (action == null) continue;

            action.Execute();
        }
    }
}
