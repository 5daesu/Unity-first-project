using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropComponent1 : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject unitObject;
    [SerializeField] private GameObject blankObject;

    private Vector2 originalPosition;

    private RectTransform bo_rectTransform;

    private Image blankImage;

    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private CanvasGroup bo_canvasGroup;

    private void Awake()
    {
        
    }

    private void Start()
    {
        blankImage = blankObject.GetComponent<Image>();
        bo_rectTransform = blankObject.GetComponent<RectTransform>();

        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        bo_canvasGroup = blankObject.GetComponent<CanvasGroup>();

        bo_canvasGroup.alpha = 0;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
        blankImage.sprite = unitObject.GetComponent<Image>().sprite;
        canvasGroup.alpha = 0.6f;
        bo_canvasGroup.alpha = 1f;


        //canvas.overrideSorting = true;
        //canvas.sortingOrder = 9;
        //canvasGroup.blocksRaycasts = false;
        //canvasGroup.alpha = 0.6f;

        Debug.Log("BeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.gameObject.name);
        bo_rectTransform.anchoredPosition += eventData.delta / 0.5f; // maybe parent's object's scale value;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        blankImage.sprite = null;
        bo_canvasGroup.alpha = 0f;

        bo_rectTransform.anchoredPosition = originalPosition;
        //canvas.overrideSorting = false;
        //canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //eventData.pointerClick.gameObject = blankObject;
        Debug.Log("OnPointerDown");
    }
}