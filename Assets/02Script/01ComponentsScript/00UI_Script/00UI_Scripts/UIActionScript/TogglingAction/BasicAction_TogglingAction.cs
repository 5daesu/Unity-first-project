using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAction : TogglingAction
{
    public override void OpenAction()
    {
        gameObject.SetActive(true);
    }

    public override void CloseAction()
    {
        gameObject.SetActive(false);
    }
}
