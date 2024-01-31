using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropComponent : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image blankImage;

    private Vector2 originalPosition;

    private RectTransform rectTransform;
    private RectTransform bi_rectTransform;

    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private CanvasGroup bi_canvasGroup;

    private void Awake()
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
}