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

    void Start(){
        
    }

    public void Swap(int v)
    {
        if(v == 0) {
            if(currentSprite != -1) {
                return;
            }
        }
        if(currentSprite == 2)
            return;
        Color oldColor = Color.white;
        if(currentSprite != -1) {
            sprites[currentSprite].enabled = false;
            oldColor = sprites[currentSprite].color;
        }
        currentSprite = v;
        sprites[currentSprite].enabled = true;
        SetColor(oldColor);
    }

    public void SetColor(Color color){
        if(currentSprite != -1){
            sprites[currentSprite].color = color;
        }
    }
}
