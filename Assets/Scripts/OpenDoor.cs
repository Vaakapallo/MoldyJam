using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OpenDoor : MonoBehaviour
{
    public Sprite openedDoor;
    private SpriteRenderer sprite;
    public Sprite cyanDoor;
    public Sprite pinkDoor;
    public Sprite purpleDoor;
    public Sprite yellowDoor;
    public Sprite yellowCyanDoor;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void DecideColors(MoldType mold1, MoldType mold2){
        if(mold2 == MoldType.None){
            if(mold1 == MoldType.Cyan){
                sprite.sprite = cyanDoor;
            } else if(mold1 == MoldType.Pink){
                sprite.sprite = pinkDoor;
            } else if(mold1 == MoldType.Purple){
                sprite.sprite = purpleDoor;
            } else if(mold1 == MoldType.Yellow){
                sprite.sprite = yellowDoor;
            }
        }
        else if(mold2 == MoldType.Cyan || mold2 == MoldType.Yellow
        && mold1 == MoldType.Cyan || mold1 == MoldType.Yellow){
            sprite.sprite = yellowCyanDoor;
        }
    }

    public void Open(){
        sprite.enabled = false;
    }
}
