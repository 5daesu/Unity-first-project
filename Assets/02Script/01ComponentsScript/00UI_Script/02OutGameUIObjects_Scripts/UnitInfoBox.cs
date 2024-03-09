using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfoBox : MonoBehaviour
{
    [SerializeField] Image unitImage;
    [SerializeField] Text unitName;

    [SerializeField] Text attackType;
    [SerializeField] Text damageType;
    [SerializeField] Text attackDamage;
    [SerializeField] Text attackSpeed;

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
