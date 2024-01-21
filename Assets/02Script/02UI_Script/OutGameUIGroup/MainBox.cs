using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    void Update()
    {
        
    }

    public void OnClickStartGame()
    {
        SceneManager.LoadScene("InGame");
    }

    public void OnClickDeckBuilding()
    {
        if (!deckBuildingBox.activeSelf) deckBuildingBox.SetActive(true);
        else deckBuildingBox.GetComponent<SmoothMove>().Callmove();
    }
}
