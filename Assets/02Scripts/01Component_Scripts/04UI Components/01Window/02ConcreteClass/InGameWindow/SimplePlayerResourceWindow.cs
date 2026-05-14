using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimplePlayerResourceWindow : NonTogglingWindow
{
    public Text playerMoneyText;
    public Text playerHpText;

    public override void UpdateWindowContent()
    {
        playerMoneyText.text = SingletonTable.singletonTable.piM.playerMoney.ToString() + " Gold";
        playerHpText.text = SingletonTable.singletonTable.piM.playerHp.ToString();
    }
}