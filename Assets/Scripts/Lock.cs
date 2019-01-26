using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tile))]
public class Lock : MonoBehaviour
{

    private Tile tile;
    // Start is called before the first frame update
    void Start()
    {
        tile = GetComponent<Tile>();
        tile.moldProof = true;
    }

    public void Unlock(){
        tile.moldProof = false;
    }
}
