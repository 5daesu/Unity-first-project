using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    [Header("Resources")]
    public int playerHp;
    public int playerMoney;

    [Header("Stats")]
    [Range(-100, 100)] public int publicSentiment;
    [Range(-100, 100)] public int luck;
    [Range(-100, 100)] public int economy;
    [Range(-100, 100)] public int morale;
    [Range(-100, 100)] public int security;
    [Range(-100, 100)] public int faith;
    [Range(-100, 100)] public int diplomacy;
    [Range(-100, 100)] public int hygiene;

    [Header("Costs")]
    public int castleCost;
    public int castleCostIncrement;  //castle cost should be increased
    public int summonCost;

    [Header("Deck")]
    public Deck playerDeck;
    public GameObject editDeckObject;
    public UnitMergeRecipeList mergeRecipeList;
    public List<GameObject> unitList;

    void Awake()
    {
        
    }

    void Start()
    {
        InitializeCurrentDeck();
    }

    private void InitializeCurrentDeck()
    {
        if (playerDeck != null) return;

        if (editDeckObject != null)
        {
            TestDeckEditor testDeckEditor = editDeckObject.GetComponent<TestDeckEditor>();
            if (testDeckEditor != null)
            {
                playerDeck = testDeckEditor.testDeck ?? testDeckEditor.BuildTestDeck();
            }
        }
    }

    void Update()
    {

    }

    public void SpendCastleCost()
    {
        ChangeMoney(-castleCost);
        UpdateCastleCost(castleCostIncrement);
    }

    private void UpdateCastleCost(int ChangeAmount)
    {
        castleCost += ChangeAmount;

        InGameUI.inGameUI.mainButton.CheckButtonState();
    }

    public void SpendSummonCost()
    {
        ChangeMoney(-summonCost);
    }

    public void ChangeMoney(int changedAmount)
    {
        playerMoney += CalculateMoneyChange(changedAmount);

        SingletonTable.singletonTable.uwM.simplePlayerResourceWindow.UpdateWindowContent();
        InGameUI.inGameUI.mainButton.CheckButtonState();
    }

    private int CalculateMoneyChange(int changedAmount)
    {
        if (changedAmount <= 0) return changedAmount;

        float economyMultiplier = Mathf.Max(0f, 1f + economy * 0.01f);
        return Mathf.RoundToInt(changedAmount * economyMultiplier);
    }

    public void ChangeHp(int changedAmount)
    {
        playerHp += changedAmount;

        SingletonTable.singletonTable.uwM.simplePlayerResourceWindow.UpdateWindowContent();
        //if (playerHp <= 0) return;
    }

    public int GetPlayerStat(PlayerStat playerStat)
    {
        switch (playerStat)
        {
            case PlayerStat.PublicSentiment:
                return publicSentiment;
            case PlayerStat.Luck:
                return luck;
            case PlayerStat.Economy:
                return economy;
            case PlayerStat.Morale:
                return morale;
            case PlayerStat.Security:
                return security;
            case PlayerStat.Faith:
                return faith;
            case PlayerStat.Diplomacy:
                return diplomacy;
            case PlayerStat.Hygiene:
                return hygiene;
            default:
                return 0;
        }
    }

    public void SetPlayerStat(PlayerStat playerStat, int value)
    {
        value = Mathf.Clamp(value, -100, 100);

        switch (playerStat)
        {
            case PlayerStat.PublicSentiment:
                publicSentiment = value;
                break;
            case PlayerStat.Luck:
                luck = value;
                break;
            case PlayerStat.Economy:
                economy = value;
                break;
            case PlayerStat.Morale:
                morale = value;
                break;
            case PlayerStat.Security:
                security = value;
                break;
            case PlayerStat.Faith:
                faith = value;
                break;
            case PlayerStat.Diplomacy:
                diplomacy = value;
                break;
            case PlayerStat.Hygiene:
                hygiene = value;
                break;
        }

        UpdatePlayerInfoWindow();
    }

    public void ChangePlayerStat(PlayerStat playerStat, int changedAmount)
    {
        SetPlayerStat(playerStat, GetPlayerStat(playerStat) + changedAmount);
    }

    private void UpdatePlayerInfoWindow()
    {
        if (SingletonTable.singletonTable == null) return;
        if (SingletonTable.singletonTable.uwM == null) return;
        if (SingletonTable.singletonTable.uwM.stageInfoWindow == null) return;

        SingletonTable.singletonTable.uwM.stageInfoWindow.UpdateWindowContent();
    }

    public void AddUnitToCurrentDeck(UnitData unitData)
    {
        if (playerDeck == null) InitializeCurrentDeck();
        if (playerDeck == null) return;

        playerDeck.AddUnit(unitData);
    }

    public bool RemoveUnitFromCurrentDeck(UnitData unitData)
    {
        if (playerDeck == null) InitializeCurrentDeck();
        if (playerDeck == null) return false;

        return playerDeck.RemoveUnit(unitData);
    }

    public bool TryFindMergeResult(IReadOnlyList<UnitData> materials, out UnitData result)
    {
        if (mergeRecipeList == null)
        {
            result = null;
            return false;
        }

        return mergeRecipeList.TryFindResult(materials, out result);
    }
}
