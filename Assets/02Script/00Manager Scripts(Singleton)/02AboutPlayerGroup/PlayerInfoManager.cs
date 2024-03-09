using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    public int playerHp;
    public int playerMoney;

    public int castleCost;
    public int castleCostIncrement;  //castle cost should be increased
    public int summonCost;

    public Deck playerDeck;
    public GameObject editDeckObject;
    public List<GameObject> unitList;

    void Awake()
    {
        
    }

    void Start()
    {
        if (playerDeck == null)
        {
            playerDeck = editDeckObject.GetComponent<TestDeckEditor>().testDeck;
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

        InGameUI.inGameUI.mainButton.CheckButtonState();
    }

    public void ChangeHp(int changedAmount)
    {
        playerHp += changedAmount;

        //if (playerHp <= 0) return;
    }
}