using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoWindow : NonTogglingWindow
{
    public Text curStageText;
    public Text publicSentimentText;
    public Text luckText;
    public Text economyText;
    public Text moraleText;
    public Text securityText;
    public Text faithText;
    public Text diplomacyText;
    public Text hygieneText;

    void OnValidate()
    {
        FindStatTexts(true);

        if (Application.isPlaying) return;

        SetText(publicSentimentText, "\uBBFC\uC2EC", 0);
        SetText(luckText, "\uD589\uC6B4", 0);
        SetText(economyText, "\uACBD\uC81C", 0);
        SetText(moraleText, "\uC0AC\uAE30", 0);
        SetText(securityText, "\uCE58\uC548", 0);
        SetText(faithText, "\uC2E0\uC559", 0);
        SetText(diplomacyText, "\uC678\uAD50", 0);
        SetText(hygieneText, "\uC704\uC0DD", 0);
    }

    public override void UpdateWindowContent()
    {
        curStageText.text = SingletonTable.singletonTable.gpM.curRound.ToString("00") + " Round";

        FindStatTexts(false);

        if (SingletonTable.singletonTable.piM == null) return;

        PlayerInfoManager playerInfoManager = SingletonTable.singletonTable.piM;

        SetText(publicSentimentText, "\uBBFC\uC2EC", playerInfoManager.publicSentiment);
        SetText(luckText, "\uD589\uC6B4", playerInfoManager.luck);
        SetText(economyText, "\uACBD\uC81C", playerInfoManager.economy);
        SetText(moraleText, "\uC0AC\uAE30", playerInfoManager.morale);
        SetText(securityText, "\uCE58\uC548", playerInfoManager.security);
        SetText(faithText, "\uC2E0\uC559", playerInfoManager.faith);
        SetText(diplomacyText, "\uC678\uAD50", playerInfoManager.diplomacy);
        SetText(hygieneText, "\uC704\uC0DD", playerInfoManager.hygiene);
    }

    private void FindStatTexts(bool renameFallbackObjects)
    {
        if (publicSentimentText == null) publicSentimentText = FindChildText(renameFallbackObjects, "PublicSentimentText", "CurrentStageText (1)");
        if (luckText == null) luckText = FindChildText(renameFallbackObjects, "LuckText", "CurrentStageText (2)");
        if (economyText == null) economyText = FindChildText(renameFallbackObjects, "EconomyText", "CurrentStageText (3)");
        if (moraleText == null) moraleText = FindChildText(renameFallbackObjects, "MoraleText", "CurrentStageText (4)");
        if (securityText == null) securityText = FindChildText(renameFallbackObjects, "SecurityText", "CurrentStageText (5)");
        if (faithText == null) faithText = FindChildText(renameFallbackObjects, "FaithText", "CurrentStageText (6)");
        if (diplomacyText == null) diplomacyText = FindChildText(renameFallbackObjects, "DiplomacyText", "CurrentStageText (7)");
        if (hygieneText == null) hygieneText = FindChildText(renameFallbackObjects, "HygieneText", "CurrentStageText (8)");
    }

    private Text FindChildText(bool renameFallbackObject, string targetName, string fallbackName)
    {
        Transform child = transform.Find(targetName);
        if (child != null && child.GetComponent<Text>() != null)
        {
            return child.GetComponent<Text>();
        }

        child = transform.Find(fallbackName);
        if (child != null && child.GetComponent<Text>() != null)
        {
            if (renameFallbackObject) child.name = targetName;

            return child.GetComponent<Text>();
        }

        return null;
    }

    private void SetText(Text text, string statName, int value)
    {
        if (text == null) return;

        text.text = statName + " " + value;
    }
}
