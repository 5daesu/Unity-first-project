using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoBox : MonoBehaviour
{
    public PlayerInfoManager piM;
    public Text playerMoneyText;

    void Update()
    {
        UpdateMoneyValue(); 
    }

    public void UpdateMoneyValue()
    {
        playerMoneyText.text = piM.playerMoney.ToString() + " Gold";
    }
}