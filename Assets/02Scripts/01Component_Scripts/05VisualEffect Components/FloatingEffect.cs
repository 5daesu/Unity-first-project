using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    public float speed;
    public float variance;

    private float delta_x;
    private Vector3 vector3;

    void Start()
    {
        Transform transform = gameObject.GetComponent<Transform>();
        
        delta_x = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Floating Effect");

        vector3 = new Vector3(0, Mathf.Sin(delta_x) * variance, 0);
        transform.localPosition = vector3;

        delta_x += Time.deltaTime * speed;
    }
}
