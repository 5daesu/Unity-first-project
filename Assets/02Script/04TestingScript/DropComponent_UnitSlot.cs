using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropComponent_UnitSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private int lv, index;
    [SerializeField] private Image deckEditBox;

    private UnitData droppedUnitData;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");

        if (eventData.pointerDrag != null)
        {
            deckEditBox.GetComponent<DeckEditorBox>().UpdateUnitSlot(lv, index, eventData.pointerDrag.transform.parent.transform.parent.GetComponent<TreeNodeObject>().unitData);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
