using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropComponent_UnitSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private int lv, index;
    [SerializeField] private Image deckEditBoxObject;

    private UnitData droppedUnitData;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");
        Debug.Log(eventData.pointerDrag.gameObject.name);

        if (eventData.pointerDrag != null)
        {
            deckEditBoxObject.GetComponent<DeckEditorBox>().UpdateUnitSlot(lv, index, eventData.pointerDrag.transform.parent.transform.parent.GetComponent<UnitDataTreeNode>().unitData);
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
