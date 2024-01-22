using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBehavior : MonoBehaviour
{
    public Slider hpSlider;

    float maxHp;
    float curHp;
    float def;
    float speed;
    int rewardMoney;

    int i;

    void Start()
    {
        maxHp = gameObject.GetComponent<MonsterInfo>().maxHp;
        def = gameObject.GetComponent<MonsterInfo>().startDef;
        speed = gameObject.GetComponent<MonsterInfo>().spd;
        rewardMoney = gameObject.GetComponent<MonsterInfo>().cost;

        curHp = maxHp;
        hpSlider.value = curHp / maxHp;
        i = 0;
    }

    void Update()
    {
        MovePath();
        CheckArrival();
    }

    void MovePath()
    {
        transform.position = Vector2.MoveTowards(transform.position, ManagerGrouping.managerGrouping.rtM.finalNodeList[i + 1].grid.transform.position, speed * Time.deltaTime);

        if (transform.position == ManagerGrouping.managerGrouping.rtM.finalNodeList[i + 1].grid.transform.position) i++;
    }

    public void GetDamage(bool attackType, float beforeDamage)
    {
        if (curHp <= 0) return; //for prevent dying twice

        float afterDamage = beforeDamage;

        //After calculating by def, ~, ~ ...

        curHp -= afterDamage;
        hpSlider.value = curHp / maxHp;

        if (curHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        ManagerGrouping.managerGrouping.paM.ChangeMoney(false, rewardMoney);
        DeActivation();
    }

    void CheckArrival()
    {
        if (transform.position == ManagerGrouping.managerGrouping.rtM.targetNode.grid.transform.position)
        {
            ManagerGrouping.managerGrouping.paM.ChangeHp(true, 1);
            DeActivation();
        }
    }

    void DeActivation()
    {
        gameObject.SetActive(false);
        ManagerGrouping.managerGrouping.stM.livingMonsters.Remove(gameObject);
        ManagerGrouping.managerGrouping.stM.CheckLeftMonster();
    }

    void OnEnable()
    {
        curHp = maxHp;
        hpSlider.value = curHp / maxHp;
        i = 0;

        gameObject.transform.position = gameObject.transform.parent.transform.position; //reset position
    }
}