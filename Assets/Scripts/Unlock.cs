using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tile))]
public class Unlock : MonoBehaviour
{
    public Lock tileToUnlock;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Tile>().OnSpread += UnlockTile;    
    }

    void UnlockTile(){
        tileToUnlock.Unlock();
    }
}
