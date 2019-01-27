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
    Pink, Cyan, Purple, Yellow, None
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

    void DetermineColor() {
        byte red = 0;
        byte green = 0;
        byte blue = 0;

        if(moldType == MoldType.Pink) {
            red = 235;
            green = 88;
            blue = 247;
        }

        if(moldType == MoldType.Cyan) {
            red = 116;
            green = 250;
            blue = 253;
        }

        if(moldType == MoldType.Purple) {
            red = 128;
            green = 55;
            blue = 214;
        }

        if(moldType == MoldType.Yellow) {
            red = 255;
            green = 250;
            blue = 83;
        }

        Color32 color = new Color32(red, green, blue, 255);

        moldManager.SetColor(color);
    }

    public void OnPointerClick (PointerEventData eventData) {
        if(GameManager.instance.clicksLeft == 0)
            return;

        Infect(GameManager.instance.chosenType, Direction.Center);
        GameManager.instance.TapSpreadAudio();
        GameManager.instance.DecreaseClicks();
        if(MoldTimerRunner.Instance != null) {
            MoldTimerRunner.Instance.ResetTimers();
        }
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
