using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private List<Tile> tiles;

    public static GameManager instance;

    void Awake(){
        instance = this;
        tiles = FindObjectsOfType<Tile>().ToList();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public List<Tile> GetNeighbours(int x, int y){
        List<Tile> neighbours = new List<Tile>();
        foreach(Tile t in tiles){
            if(t.x == x + 1 || t.x == x - 1){
                if(t.y == y){
                    neighbours.Add(t);
                }    
            }
            if(t.y == y + 1 || t.y == y - 1){
                if(t.x == x){
                    neighbours.Add(t);
                }    
            }
        }
        return neighbours;
    }


    public List<Tile> GetNeighboursBroken(int x, int y){
        return tiles.Where(tile => Mathf.Abs(tile.x - x) <= 1 
        && Mathf.Abs(tile.y - y) <= 1 
        && !(x == tile.x && y == tile.y) 
        && Mathf.Abs(tile.x - x) + Mathf.Abs(tile.y - y) < 2).ToList();
    }
}
