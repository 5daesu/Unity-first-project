using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(cr1());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(cr2());
        }
        //StopCoroutine(cr2());
    }
    IEnumerator cr1()
    {
        Debug.Log("cr1");
        yield return null;
    }

    IEnumerator cr2()
    {
        Debug.Log("cr2");
        yield return new WaitForSecondsRealtime(1f);
    }
}
