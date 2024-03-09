using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TogglingAction : MonoBehaviour
{
    public abstract void OpenAction();
    public abstract void CloseAction();
}