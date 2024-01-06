using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public static Deck playerDeck;  //singleton

    public GameObject[] deckList;

    void Awake()    //for singleton
    {
        if (Deck.playerDeck == null) Deck.playerDeck = this;
    }

    void Start()    //for temporary assignment
    {
    }
}
