using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject target;
    public float speed;

    bool atkType;
    int atkDamage;

    void Start()
    {
        target = gameObject.transform.parent.GetComponent<Attack>().enemyList[0];
        atkType = gameObject.transform.parent.GetComponent<Attack>().atkType;
        atkDamage = gameObject.transform.parent.GetComponent<Attack>().atkDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")    //it should be changed
        {
            Debug.Log("arrow hit");
            target.GetComponent<MonsterBehavior>().GetDamage(atkType, atkDamage);
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (target.activeSelf == true) transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    void OnEnable()
    {
        transform.position = transform.parent.position + new Vector3(-0.5f, 0.6f, 0);
        target = gameObject.transform.parent.GetComponent<Attack>().enemyList[0];
    }
}
