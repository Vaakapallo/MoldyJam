using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tile))]
public class Unlock : MonoBehaviour
{
    private Tile tile;
    public Lock tileToUnlock;
    public MoldType mold;

    // Start is called before the first frame update
    void Start()
    {
        tile = GetComponent<Tile>();
        tile.OnSpread += UnlockTile;    
    }

    void UnlockTile(){
        if(mold == tile.moldType)
            tileToUnlock.Unlock();
    }
}
