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

    public delegate void TileChange();
    public event TileChange OnTileChange;

    private Dictionary<Direction, MoldEdgeSpriteSwapper> directions = new Dictionary<Direction, MoldEdgeSpriteSwapper>();

    void Start() {
        directions.Add(Direction.Left, left);
        directions.Add(Direction.Right, right);
        directions.Add(Direction.Up, top);
        directions.Add(Direction.Down, bottom);
        var tile = GetComponent<Tile>();
        if(tile.moldy)
            InfectedFrom(Direction.Center);
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
            TileChanged();
            return;
        }
        StartCoroutine(WaitToEnableCenter(direction));
    }

    private void TileChanged(){
        if(OnTileChange != null){
            OnTileChange();
        }
    }

    private IEnumerator WaitToEnableCenter(Direction direction) {
        var swapper = directions[direction];
        swapper.Swap(1);
        TileChanged();
        yield return new WaitForSeconds(1f);
        swapper.Swap(2);
        center.Enable();
        foreach(var pair in directions) {
            if(pair.Key == direction)
                continue;
            pair.Value.Swap(0);
        }
        TileChanged();
    }

    public void SetColor(Color color)
    {
        foreach(var swapper in directions.Values) {
            swapper.SetColor(color);
        }
        center.SetColor(color);
    }
}
