using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject target;
    public float speed;

    bool atkType;
    int atkDamage;

    public void Initialize(GameObject target, bool atkType, int atkDamage)
    {
        this.target = target;
        this.atkType = atkType;
        this.atkDamage = atkDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (target == null || target.activeSelf == false) return;
        if (other.gameObject != target) return;

        if (other.tag == "Enemy")    //it should be changed
        {
            other.GetComponent<MonsterBehavior>().GetDamage(atkType, atkDamage);
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (target == null || target.activeSelf == false)
        {
            gameObject.SetActive(false);
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    void OnEnable()
    {
        transform.position = transform.parent.position + new Vector3(-0.5f, 0.6f, 0);
    }
}
