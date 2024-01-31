using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckEditorBox : MonoBehaviour
{
    public UnitData blankUnitData;
    [SerializeField] private Deck deck_1, deck_2, deck_3, deck_4;

    [SerializeField] Image[] lv1_Deck_Image;
    [SerializeField] Image[] lv2_Deck_Image;
    [SerializeField] Image[] lv3_Deck_Image;
    [SerializeField] Image[] lv4_Deck_Image;

    private UnitData[] lv1_Deck_UnitData;
    private UnitData[] lv2_Deck_UnitData;
    private UnitData[] lv3_Deck_UnitData;
    private UnitData[] lv4_Deck_UnitData;

    private void Awake()    //assign memory
    {
        lv1_Deck_UnitData = new UnitData[4];
        lv2_Deck_UnitData = new UnitData[4];
        lv3_Deck_UnitData = new UnitData[4];
        lv4_Deck_UnitData = new UnitData[4];
        /*
        for (int i = 0; i < 4; i++)
        {
            lv1_Deck_UnitData[i] = blankUnitData;
            lv2_Deck_UnitData[i] = blankUnitData;
            lv3_Deck_UnitData[i] = blankUnitData;
            lv4_Deck_UnitData[i] = blankUnitData;
        }
        */
    }

    public void UpdateUnitSlot(int lv, int index, UnitData droppedUnitData)
    {
        if (lv == 1)
        {
            lv1_Deck_UnitData[index] = droppedUnitData;
            lv1_Deck_Image[index].sprite = droppedUnitData.unitSprite;
        }
        else if (lv == 2)
        {
            lv2_Deck_UnitData[index] = droppedUnitData;
            lv2_Deck_Image[index].sprite = droppedUnitData.unitSprite;
        }
        else if (lv == 3)
        {
            lv3_Deck_UnitData[index] = droppedUnitData;
            lv3_Deck_Image[index].sprite = droppedUnitData.unitSprite;
        }
        else if (lv == 4)
        {
            lv4_Deck_UnitData[index] = droppedUnitData;
            lv4_Deck_Image[index].sprite = droppedUnitData.unitSprite;
        }
        else return;
    }

    public void UpdateImage(int lv, int index, Sprite unitImage)
    {
        //if (lv == 1) 
        //else if (lv == 2) lv2_Deck_Image[index].sprite = unitImage;
        //else if (lv == 3) lv3_Deck_Image[index].sprite = unitImage;
        //else if (lv == 4) lv4_Deck_Image[index].sprite = unitImage;
        //else return;
    }

    public void SaveDeck(int index)
    {
        //PlayerPrefs.se
    }

    //public void 
}
