using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType {
    Floor,
    Wall,
    Furniture
}

public class Tile : MonoBehaviour
{
    public bool moldy = true;
    private TileType type = TileType.Floor;
    public List<Tile> neighbours;

    [SerializeField] private float moldingSpeed = 0.01f;

    private float moldiness = 0f;
    private bool infected = false; 

    public bool moldProof = false;


    public int x, y;
    
    void Awake()
    {
        x = Mathf.RoundToInt(transform.localPosition.x / 0.2f);
        y = Mathf.RoundToInt(transform.localPosition.y / 0.2f);
    }

    void Start() {
        neighbours = GameManager.instance.GetNeighbours(x, y);
    }

    void Update()
    {
        if(moldy && !infected) {
            moldiness += moldingSpeed * Random.Range(0f,2f);
            moldiness = Mathf.Clamp01(moldiness);
        }
        if(moldiness >= 1 && !infected) {
            InfectNeighbours();
        }
        Color oldColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(0, moldiness, 0);
    }

    void InfectNeighbours() {
        foreach(Tile t in neighbours) {
            print("infecting");
            t.Infect();
        }
        infected = true;
    }

    void Infect(){
        if(!moldProof){
            moldy = true;
        }
    }
}
