using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfoBox : MonoBehaviour
{
    [SerializeField] private Image unitImage;
    [SerializeField] private Text unitName;

    [SerializeField] private Text attackType;
    [SerializeField] private Text damageType;
    [SerializeField] private Text attackDamage;
    [SerializeField] private Text attackSpeed;

    void Start()
    {
        
    }

    public void UpdateUnitInfoBox(UnitData unitData)
    {

        unitImage.sprite = unitData.unitPrefab.GetComponent<SpriteRenderer>().sprite;
        unitName.text = unitData.unitName;

        //attackType
        //damageType
        UnitStatus unitStatus = unitData.unitPrefab.GetComponent<UnitStatus>();
        attackDamage.text = unitStatus.attackDamage.ToString();
        attackSpeed.text = unitStatus.attackSpeed.ToString();
    }
}
