using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tile))]
public class Lock : MonoBehaviour
{
    private Tile tile;
    private List<Unlock> unlocks;

    void Awake(){
        tile = GetComponent<Tile>();
        unlocks = new List<Unlock>();
    }

    void Start()
    {
        tile.moldProof = true;
    }

    public void AddUnlocker(Unlock unlocker){
        unlocks.Add(unlocker);
    }

    public void Unlock(){
        if(AllUnlocksDone())
            tile.moldProof = false;
    }

    private bool AllUnlocksDone(){
        bool result = true; 
        foreach(Unlock unlocker in unlocks){
            if(!unlocker.GetCorrect())
                result = false;
        }
        return result;
    }
}
