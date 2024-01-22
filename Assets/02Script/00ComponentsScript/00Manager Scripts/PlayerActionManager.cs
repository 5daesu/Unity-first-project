using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    public GameObject inventoryBox;
    //public GameObject inventoryBox;

    public int playerMoney;
    public int castleCost;
    public int castleCostIncrease;  //castle cost should be increased
    public int summonCost;

    public int playerHp;

    public Deck playerDeck;

    //item
    //skill

    void Awake()
    {
        //curPlayerHp = playerHp;
    }

    void Update()   //player control
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryBox.activeSelf == false) inventoryBox.SetActive(true);
            else inventoryBox.GetComponent<SmoothMove>().Callmove();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("esc");
            inventoryBox.GetComponent<SmoothMove>().CallRemove();
        }
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
