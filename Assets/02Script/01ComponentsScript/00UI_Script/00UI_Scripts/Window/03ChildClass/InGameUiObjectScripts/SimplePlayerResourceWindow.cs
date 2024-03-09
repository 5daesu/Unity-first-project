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
        playerMoneyText.text = ManagerGrouping.managerGrouping.piM.playerMoney.ToString() + " Gold";
        playerHpText.text = ManagerGrouping.managerGrouping.piM.playerHp.ToString();
    }
}
