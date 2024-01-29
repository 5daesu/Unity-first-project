using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckEditorBox : MonoBehaviour
{
    [SerializeField] Image[] lv1_Deck_Image;
    [SerializeField] Image[] lv2_Deck_Image;
    [SerializeField] Image[] lv3_Deck_Image;
    [SerializeField] Image[] lv4_Deck_Image;

    private UnitData[] lv1_Deck_UnitData;
    private UnitData[] lv2_Deck_UnitData;
    private UnitData[] lv3_Deck_UnitData;
    private UnitData[] lv4_Deck_UnitData;

    public void UpdateUnitData(int lv, int index, UnitData droppedUnitData)
    {
        if (lv == 1) lv1_Deck_UnitData[index] = droppedUnitData;
        else if (lv == 2) lv2_Deck_UnitData[index] = droppedUnitData;
        else if (lv == 3) lv3_Deck_UnitData[index] = droppedUnitData;
        else if (lv == 4) lv4_Deck_UnitData[index] = droppedUnitData;
        else return;

        UpdateImage(lv, index, droppedUnitData.unitSprite);
    }

    public void UpdateImage(int lv, int index, Sprite unitImage)
    {
        if (lv == 1) lv1_Deck_Image[index].sprite = unitImage;
        else if (lv == 2) lv2_Deck_Image[index].sprite = unitImage;
        else if (lv == 3) lv3_Deck_Image[index].sprite = unitImage;
        else if (lv == 4) lv4_Deck_Image[index].sprite = unitImage;
        else return;
    }

    //public void 
}
