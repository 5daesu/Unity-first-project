using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public Vector3 targetPosition;
    public float speed;
    public float acceleration;

    float curSpeed;

    void Start()
    {
        curSpeed = speed;
        targetPosition = gameObject.transform.parent.GetComponent<Attack>().enemyList[0].transform.position;
    }

    void OnEnable()
    {
        targetPosition = gameObject.transform.parent.GetComponent<Attack>().enemyList[0].transform.position;
        gameObject.transform.position = targetPosition + new Vector3(0, 5f, 0);
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
        if (gameObject.transform.position == targetPosition)
        {
            Debug.Log("meteor bomb");
            gameObject.SetActive(false);
        }
    }
}
