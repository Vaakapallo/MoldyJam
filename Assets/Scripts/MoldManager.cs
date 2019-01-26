using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldManager : MonoBehaviour
{

    public MoldEdgeSpriteSwapper left;
    public MoldEdgeSpriteSwapper right;
    public MoldEdgeSpriteSwapper top;
    public MoldEdgeSpriteSwapper bottom;
    public MoldCenterSprite center;

    private Dictionary<Direction, MoldEdgeSpriteSwapper> directions = new Dictionary<Direction, MoldEdgeSpriteSwapper>();

    void Start() {
        directions.Add(Direction.Left, left);
        directions.Add(Direction.Right, right);
        directions.Add(Direction.Up, top);
        directions.Add(Direction.Down, bottom);
    }

    public void InfectTowards(Direction direction)
    {
        var swapper = directions[direction];
        swapper.Swap(2);
    }

    public void InfectedFrom(Direction direction)
    {
        if(direction == Direction.Center) {
            center.Enable();
            foreach(var swapper in directions.Values) {
                swapper.Swap(0);
            }
            return;
        }
        StartCoroutine(WaitToEnableCenter(direction));
    }

    private IEnumerator WaitToEnableCenter(Direction direction) {
        var swapper = directions[direction];
        swapper.Swap(1);
        yield return new WaitForSeconds(1f);
        swapper.Swap(2);
        center.Enable();
        foreach(var pair in directions) {
            if(pair.Key == direction)
                continue;
            pair.Value.Swap(0);
        }
        
    }
}
