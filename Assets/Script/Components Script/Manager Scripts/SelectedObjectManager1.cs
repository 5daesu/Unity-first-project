using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedObjectManager1 : MonoBehaviour
{
    public GameObject selectedObject;
    SpriteRenderer spriteRenderer;

    bool selected = false;
    bool alphaDown = false;
    bool call = false;

    void Update()
    {
        if(selected)
        {
            if (alphaDown == true) spriteRenderer.color -= new Color(0, 0, 0, 0.6f * Time.deltaTime);
            else spriteRenderer.color += new Color(0, 0, 0, 0.6f * Time.deltaTime);

            if (spriteRenderer.color.a >= 0.75f) alphaDown = true;
            else if (spriteRenderer.color.a <= 0.25f) alphaDown = false;

            if(call)
            {
                spriteRenderer.color = new Color(1, 1, 1, 1);   //original color?
                call = false;
                selected = false;
            }
        }
    }

    public void UnSelsectObject()
    {
        if (ManagerGrouping.managerGrouping.soM.selectedObject == null)   //write for -> if there's no ref for selectedObject this method will make error  
        {
            InGameUI.inGameUI.mainButton.CheckButtonState(selectedObject);
            return;
        }

        //SpriteRenderer spriteRenderer = ManagerGrouping.managerGrouping.soM.selectedObject.GetComponent<SpriteRenderer>(); //for visual effects
        //spriteRenderer.color = new Color(1, 1, 1, 1);   //original color?
        call = true;
        ManagerGrouping.managerGrouping.soM.selectedObject = null;

        InGameUI.inGameUI.mainButton.CheckButtonState(selectedObject);
    }

    public void ChangeSelected(GameObject tmp)  //will be used for singleton so it should be public
    {
        if (tmp == selectedObject)  //if user click same things it should be unselected
        {
            UnSelsectObject();
        }
        else
        {
            UnSelsectObject();
            selectedObject = tmp;
            spriteRenderer = selectedObject.GetComponent<SpriteRenderer>();
            selected = true;

            Debug.Log("New object is Selected");

            InGameUI.inGameUI.mainButton.CheckButtonState(selectedObject);
        }
    }

    /*
    IEnumerator Switch()
    {
        selected = false;
        yield return new WaitForSecondsRealtime(0.1f);
        yield break;
    }
    */
    
    /*
    IEnumerator SelectVFX(SpriteRenderer spriteRenderer)
    {
        bool alphaDown = false;
        spriteRenderer.color = new Color(1, 1, 1, 0.25f);

        while (selected)
        {
            if (spriteRenderer.color.a <= 0.5f) alphaDown = false;
            else if (spriteRenderer.color.a >= 1) alphaDown = true;

            if (alphaDown == true) spriteRenderer.color -= new Color(1, 1, 1, 0.6f * Time.deltaTime);
            else spriteRenderer.color += new Color(1, 1, 1, 0.6f * Time.deltaTime);

            yield return null;  // new WaitForSeconds(0.1f);
            if(!selected)
            {
                spriteRenderer.color = new Color(1, 1, 1, 1);
                yield break;
            }
        }
    }
    */
}
