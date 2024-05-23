using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaChangeTogglingAction : TogglingAction
{
    [SerializeField] private float colorChangingSpeed;  //5 is good
    private CanvasGroup canvasGroup;

    void Awake()
    {
        if (!GetComponent<CanvasGroup>()) canvasGroup = gameObject.AddComponent<CanvasGroup>();
        else canvasGroup = GetComponent<CanvasGroup>();
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
        canvasGroup.alpha = 0;

        if (Time.timeScale == 0)    //UI Action should not be influenced by Time.timescale
        {
            while (canvasGroup.alpha <= 1)
            {
                canvasGroup.alpha += colorChangingSpeed * Time.fixedDeltaTime * 0.12f;
                
                yield return null;
            }
            canvasGroup.alpha = 1;
        }
        else
        {
            while (canvasGroup.alpha <= 1)
            {
                canvasGroup.alpha += colorChangingSpeed * Time.deltaTime;

                yield return null;
            }
            canvasGroup.alpha = 1;
        }
        
        yield break;
    }

    IEnumerator AlphaDown()
    {
        canvasGroup.alpha = 1;

        if (Time.timeScale == 0)    //UI Action should not be influenced by Time.timescale        
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= colorChangingSpeed * Time.fixedDeltaTime * 0.12f;

                yield return null;
            }
            canvasGroup.alpha = 0;
        }
        else
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= colorChangingSpeed * Time.deltaTime;

                yield return null;
            }
            canvasGroup.alpha = 0;
        }
        
        gameObject.SetActive(false);
        yield break;
    }
}
