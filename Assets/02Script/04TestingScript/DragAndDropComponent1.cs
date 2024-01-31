using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropComponent1 : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject unitObject;
    [SerializeField] private GameObject blankObject;

    [SerializeField] private Sprite clearSprite;

    private Vector2 originalPosition;

    private RectTransform rectTransform;

    private Canvas canvas;
    private CanvasGroup unitImageCanvasGroup;

    private Image unitImage;
    private Image blankImage;

    private void Awake()
    {
        
    }

    private void Start()
    {
        //unitData = gameObject.transform.parent.transform.parent.GetComponent<TreeNodeObject>().unitData;
        originalPosition = GetComponent<RectTransform>().anchoredPosition;

        rectTransform = blankImage.GetComponent<RectTransform>();
        unitImageCanvasGroup = unitImage.GetComponent<CanvasGroup>();

        unitImage = unitObject.GetComponent<Image>();
        blankImage = blankObject.GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");
        blankImage.sprite = unitImage.sprite;
        unitImageCanvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / 0.5f; // maybe parent's object's scale value;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        blankImage.sprite = clearSprite;
        unitImageCanvasGroup.alpha = 1f;

        rectTransform.anchoredPosition = originalPosition;
        //canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
}