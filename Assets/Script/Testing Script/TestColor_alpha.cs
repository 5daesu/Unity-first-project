using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestColor_alpha : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) spriteRenderer.color = new Color(1, 1, 1, 1.1f);
        else if (Input.GetKeyDown(KeyCode.UpArrow)) spriteRenderer.color = new Color(1, 1, 1, -0.1f);
    }
}
