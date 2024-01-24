using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropComponent : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;

    private void Start()
    {

        //canvas = GameObject.FindGameObjectWithTag("OutGameMainCanvas").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();

        //canvas = canvas.rootCanvas;

        Debug.Log(canvas.name);
        Debug.Log(canvas.scaleFactor);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

   
}
