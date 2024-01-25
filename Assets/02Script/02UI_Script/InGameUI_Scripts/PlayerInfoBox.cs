using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoBox : MonoBehaviour
{
    public PlayerInfoManager piM;
    public Text playerMoneyText;
    public Text playerHpText;

    void Update()
    {
        UpdateTextValue(); 
    }

    public void UpdateTextValue()
    {
        playerMoneyText.text = piM.playerMoney.ToString() + " Gold";
        playerHpText.text = piM.playerHp.ToString();
    }
}