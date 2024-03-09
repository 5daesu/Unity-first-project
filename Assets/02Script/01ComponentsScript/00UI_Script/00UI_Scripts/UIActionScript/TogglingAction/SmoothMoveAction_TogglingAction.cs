using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMoveAction : TogglingAction
{
    [SerializeField] private float startPoint_x;
    [SerializeField] private float startPoint_y;
    private Vector3 startPoint;
    [SerializeField] private float targetPoint_x;
    [SerializeField] private float targetPoint_y;
    private Vector3 targetPoint;

    [SerializeField] private float initialSpeed;
    [SerializeField] private float removeSpeed;
    [SerializeField] private float minimumSpeed;
    
    private float currentSpeed;         //3000 is good
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
        OpenAction();
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
