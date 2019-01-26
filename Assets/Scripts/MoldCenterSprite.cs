using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldCenterSprite : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    public void Enable()
    {
        spriteRenderer.enabled = true;
    }
}
