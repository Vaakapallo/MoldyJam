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

    // Start is called before the first frame update
    void Start()
    {
        tile = GetComponent<Tile>();
        tile.OnSpread += UnlockTile;
        tileToUnlock.AddUnlocker(this);
    }

    void UnlockTile(){
        if(mold == tile.moldType){
            correct = true;
            tileToUnlock.Unlock();
        } else {
            correct = false;
        }
    }

    public bool GetCorrect(){
        return correct;
    }
}
