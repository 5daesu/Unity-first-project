using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropComponent : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject unit;
    [SerializeField] private GameObject blankObject;

    private Vector2 originalPosition;

    private RectTransform rectTransform;
    private RectTransform bo_rectTransform;

    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private CanvasGroup bo_canvasGroup;

    private void Awake()
    {
        
    }

    private void Start()
    {
        bo_canvasGroup = blankObject.GetComponent<CanvasGroup>();
        //unitData = gameObject.transform.parent.transform.parent.GetComponent<TreeNodeObject>().unitData;
        //rectTransform = GetComponent<RectTransform>();
        //bi_rectTransform = blankImage.GetComponent<RectTransform>();
        //canvas = GetComponent<Canvas>();
        //canvasGroup = GetComponent<CanvasGroup>();
        //bi_canvasGroup = blankImage.GetComponent<CanvasGroup>();

        originalPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
        //bi_canvasGroup.alpha = 0;
        rectTransform = blankObject.GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("byebye");
        //bi_rectTransform.anchoredPosition = originalPosition;
        //blankImage.sprite = gameObject.GetComponent<Image>().sprite;
        //bi_canvasGroup.alpha = 1f;

        //canvas.overrideSorting = true;
        //canvas.sortingOrder = 9;
        //canvasGroup.blocksRaycasts = false;
        //canvasGroup.alpha = 0.6f;

        //Debug.Log("BeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / 0.5f; // maybe parent's object's scale value;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //blankImage.sprite = null;
        //bi_canvasGroup.alpha = 0f;

        //rectTransform.anchoredPosition = originalPosition;
        //canvas.overrideSorting = false;
        //canvasGroup.blocksRaycasts = true;
        //canvasGroup.alpha = 1f;
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        eventData.pointerPress = blankObject;
        blankObject.GetComponent<Image>().sprite = unit.GetComponent<Image>().sprite;
    }
}

/*
 * private void Awake()
    {
        
    }

    private void Start()
    {
        //unitData = gameObject.transform.parent.transform.parent.GetComponent<TreeNodeObject>().unitData;
        rectTransform = GetComponent<RectTransform>();
        bi_rectTransform = blankImage.GetComponent<RectTransform>();
        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        bi_canvasGroup = blankImage.GetComponent<CanvasGroup>();

        originalPosition = rectTransform.anchoredPosition;
        bi_canvasGroup.alpha = 0;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        bi_rectTransform.anchoredPosition = originalPosition;
        blankImage.sprite = gameObject.GetComponent<Image>().sprite;
        bi_canvasGroup.alpha = 1f;

        canvas.overrideSorting = true;
        canvas.sortingOrder = 9;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;

        Debug.Log("BeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / 0.5f; // maybe parent's object's scale value;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        blankImage.sprite = null;
        bi_canvasGroup.alpha = 0f;

        rectTransform.anchoredPosition = originalPosition;
        canvas.overrideSorting = false;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
*/