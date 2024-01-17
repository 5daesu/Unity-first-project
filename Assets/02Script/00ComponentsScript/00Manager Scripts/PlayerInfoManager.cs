using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    public GameObject inventoryBox;
    //public GameObject inventoryBox;

    public int playerMoney;
    public int castleCost;
    public int costIncrease;    //castle cost should be increased
    public int summonCost;
    //private int castleNumber;

    public Deck playerDeck;

    //item
    //skill

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
        UpdateCastleCost();
    }

    private void UpdateCastleCost()
    {
        castleCost += costIncrease;
        InGameUI.inGameUI.mainButton.CheckButtonState();
    }

    public void SpendSummonCost()
    {
        ChangeMoney(true, summonCost);
    }

    public void ChangeMoney(bool sign, int changedAmount)  //
    {
        if (sign == false) playerMoney += changedAmount;
        else playerMoney -= changedAmount;

        InGameUI.inGameUI.mainButton.CheckButtonState();
    }
}
