using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropAction : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;

    private void Awake()
    {
        
    }

    private void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.gameObject.name);
        rectTransform.anchoredPosition += eventData.delta; // maybe parent's object's scale value;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //eventData.pointerClick.gameObject = blankObject;
        Debug.Log("OnPointerDown");
    }
}