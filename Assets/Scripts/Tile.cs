using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TileType {
    Floor,
    Wall,
    Furniture
}

public enum MoldType {
    Green, Red, Blue, Yellow
}

public class Tile : MonoBehaviour, IPointerClickHandler
{
    public bool moldy = true;
    public MoldType moldType = MoldType.Green;
    private TileType type = TileType.Floor;
    public List<Tile> neighbours;

    [SerializeField] private float moldingSpeed = 0.01f;

    private float moldiness = 0f;
    private bool hasSpread = false; 
    public delegate void SpreadAction();
    public event SpreadAction OnSpread;

    public bool moldProof = false;

    public int X;
    public int Y;
    
    void Start() {
        neighbours = GameManager.instance.GetNeighbours(X, Y);
    }

    void FixedUpdate()
    {
        if(moldy && !hasSpread && !moldProof) {
            moldiness += moldingSpeed * Random.Range(0f,2f);
            moldiness = Mathf.Clamp01(moldiness);
        }
        if(moldiness >= 1 && !hasSpread) {
            InfectNeighbours();
        }
        DetermineColor();
    }

    void DetermineColor(){
        float red = 0;
        float green = 0;
        float blue = 0;
        if(moldType == MoldType.Green)
            green = moldiness;
        if(moldType == MoldType.Red)
            red = moldiness;
        if(moldType == MoldType.Blue)
            blue = moldiness;
        if(moldType == MoldType.Yellow){
            green = moldiness;
            red = moldiness;
        }

        GetComponent<SpriteRenderer>().color = new Color(red, green, blue);
    }

    public void OnPointerClick (PointerEventData eventData) {
        Infect();     
    }

    private void InfectNeighbours() {
        foreach(Tile t in neighbours) {
            t.Infect(moldType);
        }
        hasSpread = true;
        if(OnSpread != null){
            OnSpread();
        }
    }

    private void Infect(){
        Infect(MoldType.Green);
    }

    private void Infect(MoldType type) {
        moldy = true;
        moldType = type;
    }
}
