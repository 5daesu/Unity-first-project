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
        onAttack = false;
        atkDamage = gameObject.GetComponent<UnitStatus>().attackDamage;
        atkRange = gameObject.GetComponent<UnitStatus>().attackRange;
        atkTerm = gameObject.GetComponent<UnitStatus>().attackSpeed;

        timer = 0;
        gameObject.GetComponent<CircleCollider2D>().radius = atkRange;
        enemyList = new List<GameObject>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("There is collision");
        if (other.tag == "Enemy")
        {
            Debug.Log("사거리 안으로 진입");
            onAttack = true;
            enemyList.Add(other.gameObject);
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
        if (enemyList.Count < 1)
        {
            onAttack = false;
            Debug.Log("공격상태해제");
            timer = 0;
        }
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
        //state = ~~~;
        Debug.Log("실행");
        ManagerGrouping.managerGrouping.opM.GetObject(poolingIndex, gameObject);
    }
}
