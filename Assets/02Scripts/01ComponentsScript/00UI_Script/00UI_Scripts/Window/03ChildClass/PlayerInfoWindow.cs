using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoWindow : MonoBehaviour
{
    public Text playerMoneyText;
    public Text playerHpText;

    void Update()
    {
        UpdateTextValue(); 
    }

    public void UpdateTextValue()
    {
        playerMoneyText.text = ManagerGrouping.managerGrouping.piM.playerMoney.ToString() + " Gold";
        playerHpText.text = ManagerGrouping.managerGrouping.piM.playerHp.ToString();
    }
}