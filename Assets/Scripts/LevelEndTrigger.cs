using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tile))]
public class LevelEndTrigger : MonoBehaviour
{
    private Tile tile;
    private bool finished = false;

    void Awake() {
        tile = GetComponent<Tile>();
    }

    void Update() {
        if(tile.moldType != MoldType.None && !finished) {
            GameManager.instance.FinishLevel();
            finished = true;
        }
    }

}
