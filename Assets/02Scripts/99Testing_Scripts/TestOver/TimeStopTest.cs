/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopTest : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("timestoptest awake");
    }

    void Start()
    {
        Debug.Log("timestoptest start");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("timestoptest update");

        StartCoroutine(TestCoroutine());

        Debug.Log("delta time : " + Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Debug.Log("timestoptest fixedUpdate");
    }

    IEnumerator TestCoroutine()
    {
        Debug.Log("coroutine is running");
        yield break;
    }
}
*/

//Result
//Although TimeScale = 0, without "FixedUpdated()" the rest is running.
//My mistake is about delta time
//If TimeScale = 0, Surely delta time is 0.