using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int poolingIndex;
    public bool atkType;
    public int atkDamage;
    float atkRange;
    float atkTerm;      //attack speed      the smaller, the faster
    bool onAttack;      //attacking state
    float timer;        //for checking time
    public List<GameObject> enemyList;

    void Start()
    {
        UnitStatus unitStatus = gameObject.GetComponent<UnitStatus>();

        onAttack = false;
        atkType = unitStatus.attackType != '0';
        atkDamage = unitStatus.attackDamage;
        atkRange = unitStatus.attackRange;
        atkTerm = unitStatus.attackSpeed;

        timer = 0;
        gameObject.GetComponent<CircleCollider2D>().radius = atkRange;
        enemyList = new List<GameObject>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("There is collision");
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy entered attack range");
            onAttack = true;
            if (!enemyList.Contains(other.gameObject)) enemyList.Add(other.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemyList.Remove(other.gameObject);
            CheckEnemyList();
        }
    }

    void CheckEnemyList()
    {
        RemoveInvalidEnemies();

        if (enemyList.Count < 1)
        {
            onAttack = false;
            Debug.Log("Attack state released");
            timer = 0;
        }
    }

    void RemoveInvalidEnemies()
    {
        enemyList.RemoveAll(enemy => enemy == null || enemy.activeSelf == false);
    }

    GameObject GetTarget()
    {
        RemoveInvalidEnemies();
        if (enemyList.Count < 1)
        {
            CheckEnemyList();
            return null;
        }

        return enemyList[0];
    }

    void Update()
    {
        if (onAttack == true)
        {
            timer += Time.deltaTime;
            if (timer > atkTerm)
            {
                timer = 0;
                AttckMoment();
            }
        }
    }

    void AttckMoment()  //Just the time( because of animating )
    {
        GameObject target = GetTarget();
        if (target == null) return;

        //state = ~~~;
        Debug.Log("Attack");
        GameObject attackObject = SingletonTable.singletonTable.opM.GetObject(poolingIndex, gameObject);

        Arrow arrow = attackObject.GetComponent<Arrow>();
        if (arrow != null)
        {
            arrow.Initialize(target, atkType, atkDamage);
            return;
        }

        Meteor meteor = attackObject.GetComponent<Meteor>();
        if (meteor != null)
        {
            meteor.Initialize(target.transform.position, atkType, atkDamage);
        }
    }
}
