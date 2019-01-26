using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tile))]
public class LevelEndTrigger : MonoBehaviour
{
    
    public MoldType targetType = MoldType.Green;

    private Tile tile;

    void Awake() {
        tile = GetComponent<Tile>();
    }

    void Update() {
        if(tile.moldType == targetType) {
            GameManager.instance.FinishLevel();
        }
    }

}
