using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMoveTogglingAction : TogglingAction
{
    [SerializeField] private float startPoint_x;
    [SerializeField] private float startPoint_y;
    private Vector3 startPoint;
    [SerializeField] private float targetPoint_x;
    [SerializeField] private float targetPoint_y;
    private Vector3 targetPoint;

    [SerializeField] private float initialSpeed;    //3000 is good, i think it is also important the StartPoint & TargetPoint, and proper speed has relation with that
    [SerializeField] private float removeSpeed;     //2000 is good
    [SerializeField] private float minimumSpeed;    //250 is good
    
    private float currentSpeed;
    private RectTransform rectTransform;

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
    }

    public override void OpenAction()
    {
        StopAllCoroutines();
        currentSpeed = initialSpeed;
        StartCoroutine(Move());
    }

    public override void CloseAction()
    {
        StopAllCoroutines();
        currentSpeed = removeSpeed;
        StartCoroutine(ReMove());
    }

    IEnumerator Move()
    {
        if (Time.timeScale == 0)    //UI Action should not be influenced by Time.timescale
        {
            while (rectTransform.localPosition != targetPoint)
            {
                rectTransform.localPosition = Vector3.MoveTowards(rectTransform.localPosition, targetPoint, currentSpeed * Time.fixedDeltaTime * 0.12f);
                currentSpeed = initialSpeed * (Vector3.Magnitude(targetPoint - rectTransform.localPosition) / Vector3.Magnitude(targetPoint - startPoint));
                if (currentSpeed < minimumSpeed) currentSpeed = minimumSpeed;

                yield return null;
            }
        }
        else
        {
            while (rectTransform.localPosition != targetPoint)
            {
                rectTransform.localPosition = Vector3.MoveTowards(rectTransform.localPosition, targetPoint, currentSpeed * Time.deltaTime);
                currentSpeed = initialSpeed * (Vector3.Magnitude(targetPoint - rectTransform.localPosition) / Vector3.Magnitude(targetPoint - startPoint));
                if (currentSpeed < minimumSpeed) currentSpeed = minimumSpeed;

                yield return null;
            }
        }
            

        yield break;
    }

    IEnumerator ReMove()
    {
        if (Time.timeScale == 0)    //UI Action should not be influenced by Time.timescale
        {
            while (rectTransform.localPosition != startPoint)
            {
                rectTransform.localPosition = Vector3.MoveTowards(rectTransform.localPosition, startPoint, currentSpeed * Time.fixedDeltaTime * 0.12f);
                currentSpeed = initialSpeed * (Vector3.Magnitude(startPoint - rectTransform.localPosition) / Vector3.Magnitude(startPoint - targetPoint));
                if (currentSpeed < minimumSpeed) currentSpeed = minimumSpeed;

                yield return null;
            }
        }
        else
        {
            while (rectTransform.localPosition != startPoint)
            {
                rectTransform.localPosition = Vector3.MoveTowards(rectTransform.localPosition, startPoint, currentSpeed * Time.deltaTime);
                currentSpeed = initialSpeed * (Vector3.Magnitude(startPoint - rectTransform.localPosition) / Vector3.Magnitude(startPoint - targetPoint));
                if (currentSpeed < minimumSpeed) currentSpeed = minimumSpeed;

                yield return null;
            }
        }
            

        gameObject.SetActive(false);
        yield break;
    }
}