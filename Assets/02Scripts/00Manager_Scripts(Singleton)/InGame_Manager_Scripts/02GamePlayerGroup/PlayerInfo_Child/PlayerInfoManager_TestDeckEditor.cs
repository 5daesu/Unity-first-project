using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDeckEditor : MonoBehaviour  //Editing Deck should be did in Game but sometime there's need to do it in Unity Editor
{
    public Deck testDeck;   //If there's some Exception it will be used by Basic Deck

    [SerializeField] private List<UnitData> testUnitData;

    private void Awake()
    {
        BuildTestDeck();
    }

    public Deck BuildTestDeck()
    {
        testDeck = new Deck();
        testDeck.AddUnits(testUnitData);
        return testDeck;
    }
}
