using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputManager : MonoBehaviour
{
    public GameObject inventoryBox;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryBox.activeSelf == false) inventoryBox.SetActive(true);
            else inventoryBox.GetComponent<SmoothMove>().Callmove();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("esc");
            inventoryBox.GetComponent<SmoothMove>().CallRemove();
        }
    }
}
