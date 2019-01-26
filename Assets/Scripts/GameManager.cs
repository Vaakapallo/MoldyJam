using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public MoldType chosenType = MoldType.Green;
    public Slider timeScaleSlider;

    private List<Tile> tiles;

    public static GameManager instance;

    [Range(1,50)]
    public float timeScale = 1.0f;

    void Awake(){
        instance = this;
        tiles = FindObjectsOfType<Tile>().ToList();
        timeScaleSlider.onValueChanged.AddListener(val => ChangeTimescale(val));
    }
    
    public List<Tile> GetNeighbours(int x, int y) {
        List<Tile> neighbours = new List<Tile>();
        foreach(Tile t in tiles){
            if(t.X == x + 1 || t.X == x - 1) {
                if(t.Y == y){
                    neighbours.Add(t);
                }    
            }
            if(t.Y == y + 1 || t.Y == y - 1) {
                if(t.X == x) {
                    neighbours.Add(t);
                }    
            }
        }
        return neighbours;
    }

    public void ChangeToGreen() {
        chosenType = MoldType.Green;
    }
    public void ChangeToBlue() {
        chosenType = MoldType.Blue;
    }
    public void ChangeToRed() {
        chosenType = MoldType.Red;
    }
    public void ChangeToYellow() {
        chosenType = MoldType.Yellow;
    }

    private void ChangeTimescale(float timeScale) {
        Time.timeScale = timeScale;
    }

    public List<Tile> GetNeighboursBroken(int x, int y){
        return tiles.Where(tile => Mathf.Abs(tile.X - x) <= 1 
        && Mathf.Abs(tile.Y - y) <= 1 
        && !(x == tile.X && y == tile.Y) 
        && Mathf.Abs(tile.X - x) + Mathf.Abs(tile.Y - y) < 2).ToList();
    }
}
