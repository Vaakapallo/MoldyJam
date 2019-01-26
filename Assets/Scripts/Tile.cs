using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public float moldiness = 0f;
    public bool moldy = true;
    private string type = "Floor";
    public List<Tile> neighbours;

    private bool infected = false; 

    public int x,y;
    
    // Start is called before the first frame update
    void Awake()
    {
        x = Mathf.RoundToInt(transform.localPosition.x/0.2f);
        y = Mathf.RoundToInt(transform.localPosition.y/0.2f);

    }

    void Start(){
        neighbours = GameManager.instance.GetNeighbours(x, y);
    }

    // Update is called once per frame
    void Update()
    {
        if(moldy){
            moldiness += 0.01f * Random.Range(0f,2f);
        }
        if(moldiness >= 1 && !infected){
            InfectNeighbours();
        }
        Color oldColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(0, moldiness, 0);
    }

    void InfectNeighbours(){
        foreach(Tile t in neighbours){
            print("infecting");
            t.moldy = true;
        }
        infected = true;
    }
}
