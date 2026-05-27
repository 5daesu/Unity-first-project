using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuildingBox : MonoBehaviour
{
    public Button exitButton;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickExitButton()
    {
        gameObject.GetComponent<SmoothMove>().CallRemove();
    }
}
