using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmoothMove : MonoBehaviour     //caution! : When values of pivot and anchor are 0.5, this script run well.     // i dont know why...
{
    public float startPoint_x;
    public float startPoint_y;
    Vector3 startPoint;
    public float targetPoint_x;
    public float targetPoint_y;
    Vector3 targetPoint;

    public float initialSpeed;
    public float removeSpeed;
    public float minimumSpeed;
    float currentSpeed;         //3000 is good

    RectTransform rectTransform;

    void Awake()
    {
        startPoint = new Vector3(startPoint_x, startPoint_y, 0);
        targetPoint = new Vector3(targetPoint_x, targetPoint_y, 0);
        currentSpeed = initialSpeed;

        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        rectTransform.localPosition = startPoint;
        Debug.Log(rectTransform.localPosition.x + " " + rectTransform.localPosition.y + " " + rectTransform.localPosition.z);
        currentSpeed = initialSpeed;
        //StartCoroutine(Move());
        Callmove();
    }

    public void Callmove()
    {
        StopAllCoroutines();
        currentSpeed = initialSpeed;
        StartCoroutine(Move());
    }

    public void CallRemove()
    {
        StopAllCoroutines();
        currentSpeed = removeSpeed;
        StartCoroutine(ReMove());
    }

    IEnumerator Move()
    {
        while (rectTransform.localPosition != targetPoint)
        {
            rectTransform.localPosition = Vector3.MoveTowards(rectTransform.localPosition, targetPoint, currentSpeed * Time.deltaTime);
            currentSpeed = initialSpeed * (Vector3.Magnitude(targetPoint - rectTransform.localPosition) / Vector3.Magnitude(targetPoint - startPoint));
            if (currentSpeed < minimumSpeed) currentSpeed = minimumSpeed;
            
            yield return null;
        }

        yield break;
    }

    IEnumerator ReMove()
    {
        Debug.Log("Remove");
        while (rectTransform.localPosition != startPoint)
        {
            rectTransform.localPosition = Vector3.MoveTowards(rectTransform.localPosition, startPoint, currentSpeed * Time.deltaTime);
            currentSpeed = initialSpeed * (Vector3.Magnitude(startPoint - rectTransform.localPosition) / Vector3.Magnitude(startPoint - targetPoint));
            if (currentSpeed < minimumSpeed) currentSpeed = minimumSpeed;

            yield return null;
        }

        gameObject.SetActive(false);
        yield break;
    }
}