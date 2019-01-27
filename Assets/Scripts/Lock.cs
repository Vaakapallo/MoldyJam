using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tile))]
public class Lock : MonoBehaviour
{
    private Tile tile;
    private List<Unlock> unlocks;
    private GameObject DoorSprite;
    private OpenDoor open;

    void Awake(){
        tile = GetComponent<Tile>();
        unlocks = new List<Unlock>();
        DoorSprite = Resources.Load<GameObject>("DoorFrame");
    }

    void Start()
    {
        open = Instantiate(DoorSprite,this.transform).GetComponent<OpenDoor>();
        tile.moldProof = true;
        Invoke("GetColorsAndSendToDoor", 0.03f);
    }

    private void GetColorsAndSendToDoor(){
        List<MoldType> molds = new List<MoldType>();
        foreach(Unlock u in unlocks){
            if(!molds.Contains(u.mold))
                molds.Add(u.mold);
        }
        if(molds.Count > 1){
            open.DecideColors(molds[0], molds[1]);
        } else {
            open.DecideColors(molds[0], MoldType.None);
        }
    }

    public void AddUnlocker(Unlock unlocker){
        unlocks.Add(unlocker);
    }

    public void Unlock(){
        if(AllUnlocksDone()){
            tile.moldProof = false;
            open.Open();
        }
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
