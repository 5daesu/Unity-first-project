using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public Vector3 targetPosition;
    public float speed;
    public float acceleration;
    public float damageRadius = 0.75f;

    float curSpeed;
    bool atkType;
    int atkDamage;

    public void Initialize(Vector3 targetPosition, bool atkType, int atkDamage)
    {
        this.targetPosition = targetPosition;
        this.atkType = atkType;
        this.atkDamage = atkDamage;
        transform.position = targetPosition + new Vector3(0, 5f, 0);
        curSpeed = speed;
    }

    void OnEnable()
    {
        curSpeed = speed;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, curSpeed * Time.deltaTime);
        curSpeed += acceleration;
        CheckArrival();
    }

    void CheckArrival()
    {
        if (Vector3.Distance(gameObject.transform.position, targetPosition) <= 0.01f)
        {
            Debug.Log("meteor bomb");
            ApplyDamage();
            gameObject.SetActive(false);
        }
    }

    void ApplyDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(targetPosition, damageRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.tag != "Enemy") continue;

            MonsterBehavior monsterBehavior = hit.GetComponent<MonsterBehavior>();
            if (monsterBehavior != null) monsterBehavior.GetDamage(atkType, atkDamage);
        }
    }
}
