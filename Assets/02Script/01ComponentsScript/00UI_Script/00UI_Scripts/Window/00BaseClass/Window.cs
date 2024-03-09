using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour
{
    public Canvas canvas { get; set; }
    public bool isActive { get; set; }

    abstract public void UpdateWindowContent();
}