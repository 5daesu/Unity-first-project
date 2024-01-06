using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoWindow : MonoBehaviour
{
    public Text PlayerMoney;

    void Update()
    {
        UpdateMoneyValue(); 
    }

    public void UpdateMoneyValue()
    {
        PlayerMoney.text = ManagerGrouping.managerGrouping.piM.playerMoney.ToString() + " Gold";
    }
}
