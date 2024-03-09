using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBehavior : MonoBehaviour
{
    public Image HpBar;

    float maxHp;
    float curHp;
    float atk;
    float def;
    float speed;
    int rewardMoney;

    int i;

    private void Awake()
    {
    }

    void Start()
    {

        maxHp = gameObject.GetComponent<MonsterInfo>().maxHp;
        def = gameObject.GetComponent<MonsterInfo>().startDef;
        speed = gameObject.GetComponent<MonsterInfo>().spd;
        rewardMoney = gameObject.GetComponent<MonsterInfo>().cost;

        curHp = maxHp;
        HpBar.transform.localScale = new Vector3(curHp / maxHp, 1, 1);
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
        HpBar.transform.localScale = new Vector3(curHp / maxHp, 1, 1);

        if (curHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        ManagerGrouping.managerGrouping.piM.ChangeMoney(rewardMoney);
        DeActivation();
    }

    void CheckArrival()
    {
        if (transform.position == ManagerGrouping.managerGrouping.rtM.targetNode.grid.transform.position)
        {
            ManagerGrouping.managerGrouping.piM.ChangeHp(-1);
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
        HpBar.transform.localScale = new Vector3(1, 1, 1);
        i = 0;

        gameObject.transform.position = gameObject.transform.parent.transform.position; //reset position
    }
}