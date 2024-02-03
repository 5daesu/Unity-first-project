using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TreeNodeObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int lv, index;

    public UnitData unitData;
    public int unitCode;
    
    public Image unitImage;
    private bool isSelected;

    void Start()
    {
    }

    /*
    public void OnClickTreeNode()
    {
        Debug.Log(unitData.unitName);
        if (unitData.unitLevel == 1) Debug.Log("Unit Level is 1, There's No Recipe");
        else Debug.Log("Unit Level is " + unitData.unitLevel);
    }
    */

    public void MarkTreeNode()
    {
        Debug.Log("Marking");
        unitImage.sprite = null;
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
            gameObject.transform.parent.GetComponent<TreeNodeSpace>().FindChildTreeNode(unitData);
            gameObject.transform.parent.GetComponent<TreeNodeSpace>().FindParentTreeNode(unitData);
        }
        else
        {
            isSelected = false;
        }
    }
}