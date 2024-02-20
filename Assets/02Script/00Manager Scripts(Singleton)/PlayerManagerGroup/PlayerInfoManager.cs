using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    public int playerHp;
    public int playerMoney;

    public int castleCost;
    public int castleCostIncrease;  //castle cost should be increased
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
        ChangeMoney(true, castleCost);
        UpdateCastleCost(castleCostIncrease);
    }

    private void UpdateCastleCost(int ChangeAmount)
    {
        castleCost += ChangeAmount;
        InGameUI.inGameUI.mainButton.CheckButtonState();
    }

    public void SpendSummonCost()
    {
        ChangeMoney(true, summonCost);
    }

    public void ChangeMoney(bool sign, int changedAmount)
    {
        if (sign == false) playerMoney += changedAmount;
        else playerMoney -= changedAmount;

        InGameUI.inGameUI.mainButton.CheckButtonState();
    }

    public void ChangeHp(bool sign, int changedAmount)
    {
        if (sign == false) playerHp += changedAmount;
        else playerHp -= changedAmount;
    }
}