using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditDeckComponent : MonoBehaviour  //Editing Deck should be did in Game but sometime i need do it by script
{
    public Deck testDeck;   //it is used for Exception

    [SerializeField] UnitData[] lv_1_unitData;
    [SerializeField] UnitData[] lv_2_unitData;
    [SerializeField] UnitData[] lv_3_unitData;
    [SerializeField] UnitData[] lv_4_unitData;

    private void Start()
    {
        //testDeck.AssignMemory();

        for (int i = 0; i < 4; i++)
        {
            testDeck.deckData[0, i] = lv_1_unitData[i];
            //basicDeck.deckData[1, i] = lv_2_unitData[i];
            //basicDeck.deckData[2, i] = lv_3_unitData[i];
            //basicDeck.deckData[3, i] = lv_4_unitData[i];
        }

        testDeck.UpdateTable();
    }
}
