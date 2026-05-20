using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    [Header("Resources")]
    public int playerHp;
    public int playerMoney;

    [Header("Stats")]
    public int publicSentiment;
    public int luck;
    public int economy;
    public int morale;
    public int security;
    public int faith;
    public int diplomacy;

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
        playerMoney += changedAmount;

        SingletonTable.singletonTable.uwM.simplePlayerResourceWindow.UpdateWindowContent();
        InGameUI.inGameUI.mainButton.CheckButtonState();
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
            default:
                return 0;
        }
    }

    public void SetPlayerStat(PlayerStat playerStat, int value)
    {
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
        }
    }

    public void ChangePlayerStat(PlayerStat playerStat, int changedAmount)
    {
        SetPlayerStat(playerStat, GetPlayerStat(playerStat) + changedAmount);
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
