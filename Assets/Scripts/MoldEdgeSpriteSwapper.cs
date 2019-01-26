using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldEdgeSpriteSwapper : MonoBehaviour
{

    private SpriteRenderer[] sprites;
    private int currentSprite = -1;

    void Awake()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }


    public void Swap(int v)
    {
        if(v == 0) {
            if(currentSprite != -1) {
                return;
            }
        }

        if(currentSprite != -1) {
            sprites[currentSprite].enabled = false;
        }
        currentSprite = v;
        sprites[currentSprite].enabled = true;

    }
}
