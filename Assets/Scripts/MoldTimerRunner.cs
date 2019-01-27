using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoldTimerRunner : MonoBehaviour
{
    
    public static MoldTimerRunner Instance;

    private Dictionary<int, float> numOfMoldToTime = new Dictionary<int, float>() { { 2, 0f }, { 3, 0f }, { 4, 0f } };
    private Dictionary<int, float> records = new Dictionary<int, float>() { { 2, -1f }, { 3, -1f }, { 4, -1f } };

    private GameManager gameManager;

    void Awake() {
        gameManager = GameManager.instance;
        Instance = this;
    }

    public void ResetTimers() {
        foreach(var amount in numOfMoldToTime.Keys) {
            numOfMoldToTime[amount] = 0f;
        }
    }

    void Update() {
        if(gameManager == null){
            return;
        }
        if(gameManager.tiles == null){
            return;
        }
        var distinctColors = gameManager.tiles.Select( tile => tile.moldType ).Distinct().Count();
        if(distinctColors > 1) {
            for(int i = 2; i <= distinctColors; ++i) {
                numOfMoldToTime[i] += Time.deltaTime;
                var record = records[i];
                if( record < 0f || numOfMoldToTime[i] < record ) {
                    records[i] = numOfMoldToTime[i];
                }
            }
        }
    }

}
