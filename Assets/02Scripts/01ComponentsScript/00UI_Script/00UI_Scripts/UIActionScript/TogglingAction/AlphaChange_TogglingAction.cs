using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaChangeTogglingAction : TogglingAction
{
    [SerializeField] private float colorChangingSpeed;  //5 is good
    private Image image;
    private Color tmpColor;   //Its for calculating and assignment  //More explain : For example "image.color" is not variable -> So we can't change its value, but we can assign -> But tmpColor is variable, So we can change its value 

    void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

    public override void OpenAction()
    {
        StopAllCoroutines();
        StartCoroutine(AlphaUp());
    }

    public override void CloseAction()
    {
        StopAllCoroutines();
        StartCoroutine(AlphaDown());
    }

    IEnumerator AlphaUp()
    {
        tmpColor = image.color;     //reset tmpColor
        tmpColor.a = 0;             //additional_work for reset 

        while (image.color.a <= 1)
        {
            tmpColor.a += colorChangingSpeed * Time.deltaTime;
            image.color = tmpColor;

            yield return null;
        }
        tmpColor.a = 1;
        image.color = tmpColor;

        yield break;
    }

    IEnumerator AlphaDown()
    {
        tmpColor = image.color;     //reset tmpColor
        tmpColor.a = 1;             //additional_work for reset 

        while (image.color.a >= 0)
        {
            tmpColor.a -= colorChangingSpeed * Time.deltaTime;
            image.color = tmpColor;

            yield return null;
        }
        tmpColor.a = 0;
        image.color = tmpColor;

        gameObject.SetActive(false);
        yield break;
    }
}
