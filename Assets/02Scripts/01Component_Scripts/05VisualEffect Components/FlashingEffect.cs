using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXselectedobject : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    bool alphaDown;
    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (alphaDown == true) spriteRenderer.color -= new Color(0, 0, 0, 1f * Time.deltaTime);
        else spriteRenderer.color += new Color(0, 0, 0, 1f * Time.deltaTime);

        if (spriteRenderer.color.a >= 0.75f) alphaDown = true;
        else if (spriteRenderer.color.a <= 0.25f) alphaDown = false;
    }
}
