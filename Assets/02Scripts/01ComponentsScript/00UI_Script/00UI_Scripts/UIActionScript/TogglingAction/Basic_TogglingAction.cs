using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTogglingAction : TogglingAction
{
    public override void OpenAction()
    {
    }

    public override void CloseAction()
    {
        gameObject.SetActive(false);
    }
}
