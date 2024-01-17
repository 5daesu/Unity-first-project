using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainBox : MonoBehaviour
{
    public Button enterGame;

    public Button deckBuilding;
    public GameObject deckBuildingBox;

    public Button enterstore;

    public Button exitGame;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickEnterGame()
    {

    }

    public void OnClickDeckBuilding()
    {
        deckBuildingBox.SetActive(true);
    }
}
