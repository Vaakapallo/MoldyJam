using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tile))]
public class Unlock : MonoBehaviour
{
    private Tile tile;
    public Lock tileToUnlock;
    public MoldType mold;
    private bool correct = false;
    private GameObject UnlockSprite;
    private UnlockerSprite unlocker;

    void Awake()
    {
        UnlockSprite = Resources.Load<GameObject>("UnlockerObject");
    }

    // Start is called before the first frame update
    void Start()
    {
        unlocker = Instantiate(UnlockSprite,this.transform).GetComponent<UnlockerSprite>();
        tile = GetComponent<Tile>();
        tile.OnSpread += UnlockTile;
        tileToUnlock.AddUnlocker(this);
        unlocker.DecideColor(mold);
    }

    void UnlockTile(){
        if(mold == tile.moldType) {
            correct = true;
            tileToUnlock.Unlock();
            GameManager.instance.UnlockDoorAudio();
        } else {
            correct = false;
        }
    }

    public bool GetCorrect(){
        return correct;
    }
}
