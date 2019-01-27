using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OpenDoor : MonoBehaviour
{
    
    public Sprite openedDoor;
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Open(){
        sprite.sprite = openedDoor;
    }
}
