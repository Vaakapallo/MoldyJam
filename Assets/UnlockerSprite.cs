using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockerSprite : MonoBehaviour
{
    public Sprite PurpleApple;
    public Sprite CyanFridge;
    public Sprite YellowTrashBin;

    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void DecideColor(MoldType mold){
        if(mold == MoldType.Purple){
            sprite.sprite = PurpleApple;
        } else if(mold == MoldType.Cyan){
            sprite.sprite = CyanFridge;
        } else if(mold == MoldType.Yellow){
            sprite.sprite = YellowTrashBin;
        }
    }
}
