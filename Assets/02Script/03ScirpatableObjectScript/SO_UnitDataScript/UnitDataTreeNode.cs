using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitDataTreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image unitImageObject;
    [SerializeField] private Image blankImageObject;

    public int lv { get; set; }
    public int index { get; set; }

    public int unitCode { get; set; }
    public UnitData unitData { get; set; }

    private TreeNodeSpace treeNodeSpace;
    private CanvasGroup blankImageCanvasGroup;
    private bool isSelected;

    void Start()
    {
        treeNodeSpace = gameObject.transform.parent.GetComponent<TreeNodeSpace>();  //it will be executed much time, because there's much instance of this class ( so this part is not good )
        blankImageCanvasGroup = blankImageObject.GetComponent<CanvasGroup>();
        blankImageCanvasGroup.alpha = 0f;
    }

    /*
    public void OnClickTreeNode()
    {
        Debug.Log(unitData.unitName);
        if (unitData.unitLevel == 1) Debug.Log("Unit Level is 1, There's No Recipe");
        else Debug.Log("Unit Level is " + unitData.unitLevel);
    }
    */

    public void ResetUnitImage(Sprite unitSprite)
    {
        unitImageObject.sprite = unitSprite;
    }

    public void MarkTreeNode()
    {
        Debug.Log("Marking");
        blankImageCanvasGroup.alpha = 0.5f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (isSelected == false)
        {
            isSelected = true;
            treeNodeSpace.FindChildTreeNode(unitData);
            treeNodeSpace.FindParentTreeNode(unitData);
            treeNodeSpace.unitInfoBox.UpdateUnitInfoBox(unitData);
        }
        else
        {
            isSelected = false;
        }
    }
}