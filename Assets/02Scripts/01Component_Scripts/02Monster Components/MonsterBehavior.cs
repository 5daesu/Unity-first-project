using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBehavior : MonoBehaviour
{
    const int MonsterSortingOffset = 2;

    public Image HpBar;

    float maxHp;
    float curHp;
    float atk;
    float def;
    float speed;
    int rewardMoney;

    int i;
    SpriteRenderer monsterSprite;

    private void Awake()
    {
        monsterSprite = gameObject.GetComponent<SpriteRenderer>();
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
        RefreshSortingOrder();
        CheckArrival();
    }

    void MovePath()
    {
        transform.position = Vector2.MoveTowards(transform.position, SingletonTable.singletonTable.rtM.finalNodeList[i + 1].grid.transform.position, speed * Time.deltaTime);

        if (transform.position == SingletonTable.singletonTable.rtM.finalNodeList[i + 1].grid.transform.position) i++;
    }

    void RefreshSortingOrder()
    {
        if (monsterSprite == null) monsterSprite = gameObject.GetComponent<SpriteRenderer>();
        if (monsterSprite == null) return;

        int targetIndex = Mathf.Min(i + 1, SingletonTable.singletonTable.rtM.finalNodeList.Count - 1);
        Grid currentGrid = SingletonTable.singletonTable.rtM.finalNodeList[targetIndex].grid;
        monsterSprite.sortingOrder = currentGrid.GetObjectSortingOrder(MonsterSortingOffset);
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
        SingletonTable.singletonTable.piM.ChangeMoney(rewardMoney);
        DeActivation();
    }

    void CheckArrival()
    {
        if (transform.position == SingletonTable.singletonTable.rtM.targetNode.grid.transform.position)
        {
            SingletonTable.singletonTable.piM.ChangeHp(-1);
            DeActivation();
        }
    }

    void DeActivation()
    {
        gameObject.SetActive(false);
        SingletonTable.singletonTable.gpM.livingMonsters.Remove(gameObject);
        SingletonTable.singletonTable.gpM.CheckLeftMonster();
    }

    void OnEnable()
    {
        curHp = maxHp;
        HpBar.transform.localScale = new Vector3(1, 1, 1);
        i = 0;

        gameObject.transform.position = gameObject.transform.parent.transform.position; //reset position
    }
}
