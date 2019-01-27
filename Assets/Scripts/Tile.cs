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
    Green, Red, Blue, Yellow, None
}

public enum Direction {
    Left,
    Right,
    Up,
    Down,
    Center
}

public class Tile : MonoBehaviour, IPointerClickHandler
{
    public bool moldy = true;
    public MoldType moldType = MoldType.None;
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

    private MoldManager moldManager;
    
    void Start() {
        neighbours = GameManager.instance.GetNeighbours(X, Y);
        moldManager = GetComponent<MoldManager>();
        moldManager.OnTileChange += DetermineColor;
        DetermineColor();
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
    }

    void DetermineColor(){
        float red = 0;
        float green = 0;
        float blue = 0;
        if(moldType == MoldType.Green)
            green = 1;
        if(moldType == MoldType.Red)
            red = 1;
        if(moldType == MoldType.Blue)
            blue = 1;
        if(moldType == MoldType.Yellow){
            green = 1;
            red = 1;
        }

        moldManager.SetColor(new Color(red, green, blue));
    }

    public void OnPointerClick (PointerEventData eventData) {
        Infect(GameManager.instance.chosenType, Direction.Center);
        GameManager.instance.TapSpreadAudio();
    }

    private void InfectNeighbours() {
        foreach(Tile t in neighbours) {
            var direction = GetInfectionDirectionRelativeToInfected(t);
            t.Infect(moldType, direction);
            moldManager.InfectTowards(ReverseDirection(direction));
        }
        hasSpread = true;
        if(OnSpread != null){
            OnSpread();
        }
    }

    private void Infect(MoldType type, Direction direction) {
        if(moldType != type) {
            hasSpread = false;
            moldiness = 0;
        }
        moldy = true;
        moldType = type;
        moldManager.InfectedFrom(direction);
    }

    private Direction GetInfectionDirectionRelativeToInfected(Tile tile) {
        if(tile.X > this.X)
            return Direction.Left;
        if(tile.X < this.X)
            return Direction.Right;
        if(tile.Y > this.Y)
            return Direction.Down;
        if(tile.Y < this.Y)
            return Direction.Up;

        return Direction.Center;
    }

    private Direction ReverseDirection(Direction direction) {
        switch(direction) {
            case Direction.Down:
                return Direction.Up;
            case Direction.Up:
                return Direction.Down;
            case Direction.Left:
                return Direction.Right;
            case Direction.Right:
                return Direction.Left;
            default:
                return Direction.Center;
        }
    }
}
