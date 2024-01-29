using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropComponent_UnitSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Image deckEditBox;
    [SerializeField] private int lv, index;

    private UnitData droppedUnitData;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");

        if (eventData.pointerDrag != null)
        {
            deckEditBox.GetComponent<EditDeck>().UpdateImage(lv, index, eventData.pointerDrag.GetComponent<Image>().sprite);
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
