using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private const float IsometricRangeYScale = 0.5f;
    private const int AttackRangeColliderPointCount = 64;

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
        ConfigureAttackRangeCollider();
        enemyList = new List<GameObject>();
    }

    void ConfigureAttackRangeCollider()
    {
        CircleCollider2D circleCollider = gameObject.GetComponent<CircleCollider2D>();
        if (circleCollider != null) circleCollider.enabled = false;

        PolygonCollider2D rangeCollider = gameObject.GetComponent<PolygonCollider2D>();
        if (rangeCollider == null) rangeCollider = gameObject.AddComponent<PolygonCollider2D>();

        Vector2[] points = new Vector2[AttackRangeColliderPointCount];
        for (int i = 0; i < AttackRangeColliderPointCount; i++)
        {
            float angle = Mathf.PI * 2f * i / AttackRangeColliderPointCount;
            points[i] = new Vector2(
                Mathf.Cos(angle) * atkRange,
                Mathf.Sin(angle) * atkRange * IsometricRangeYScale);
        }

        rangeCollider.isTrigger = true;
        rangeCollider.pathCount = 1;
        rangeCollider.SetPath(0, points);
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
