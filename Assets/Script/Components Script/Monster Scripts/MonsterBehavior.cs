using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBehavior : MonoBehaviour
{
    public Slider hpSlider;

    float maxHp;
    float curHp;
    int def;
    float speed;
    int rewardMoney;

    int i;

    void Start()
    {
        maxHp = gameObject.GetComponent<MonsterStatus>().maxHp;
        def = gameObject.GetComponent<MonsterStatus>().def;
        speed = gameObject.GetComponent<MonsterStatus>().spd;
        rewardMoney = gameObject.GetComponent<MonsterStatus>().rewardMoney;

        curHp = maxHp;
        hpSlider.value = curHp / maxHp;
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MovePath();
        CheckArrival();
    }

    void MovePath()
    {
        transform.position = Vector2.MoveTowards(transform.position, ManagerGrouping.managerGrouping.rtM.finalNodeList[i + 1].grid.myPosition, speed * Time.deltaTime);

        if (transform.position == ManagerGrouping.managerGrouping.rtM.finalNodeList[i + 1].grid.myPosition) i++;

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
        ManagerGrouping.managerGrouping.piM.ChangeMoney(false, rewardMoney);
        DeActivation();
    }

    void CheckArrival()
    {
        if (transform.position == ManagerGrouping.managerGrouping.rtM.targetNode.grid.myPosition) DeActivation();
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
