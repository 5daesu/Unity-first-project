using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour
{
    //[SerializeField] public string windowName;      //For User
    public Canvas canvas { get; set; }
    public bool isActive { get; set; }
    //There's a similar property with "isActive", It's a "GameObject.ActiveSelf" 
    //Then why did I define this,,,
    //Because of executing something more complicated Action
    //For example : TogglingWinodw.OpenWindow() method, I think it's hard to make it without "isActive"
    
    protected virtual void Awake()
    {
        if (gameObject.GetComponent<Canvas>()) canvas = gameObject.GetComponent<Canvas>();
        else
        {
            gameObject.AddComponent<Canvas>();
            canvas = gameObject.GetComponent<Canvas>();
        }

        isActive = false;
    }

    abstract public void UpdateWindowContent();
}