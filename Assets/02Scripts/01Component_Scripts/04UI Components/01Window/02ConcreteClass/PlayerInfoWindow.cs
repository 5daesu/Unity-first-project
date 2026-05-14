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
        playerMoneyText.text = SingletonTable.singletonTable.piM.playerMoney.ToString() + " Gold";
        playerHpText.text = SingletonTable.singletonTable.piM.playerHp.ToString();
    }
}