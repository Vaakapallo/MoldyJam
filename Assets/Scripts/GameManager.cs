using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public MoldType chosenType = MoldType.Pink;
    public Slider timeScaleSlider;
    public GameObject nextLevelButton;
    public AudioClip doorUnlockAudio;
    public AudioClip tapSpreadAudio;
    public AudioClip winAudio;

    private List<Tile> tiles;
    private AudioSource audioSource;

    public static GameManager instance;

    [Range(1,50)]
    public float timeScale = 1.0f;

    void Awake(){
        instance = this;
        tiles = FindObjectsOfType<Tile>().ToList();
        timeScaleSlider.onValueChanged.AddListener(val => ChangeTimescale(val));
        audioSource = GetComponent<AudioSource>();
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

    public void ChangeToPink() {
        chosenType = MoldType.Pink;
    }
    public void ChangeToPurple() {
        chosenType = MoldType.Purple;
    }
    public void ChangeToCyan() {
        chosenType = MoldType.Cyan;
    }
    public void ChangeToYellow() {
        chosenType = MoldType.Yellow;
    }

    private void ChangeTimescale(float timeScale) {
        Time.timeScale = timeScale;
    }

    public void LoadNextLevel() {
        MainMenuManager.Instance.LoadNextLevel();
    }

    public void FinishLevel() {
        nextLevelButton.SetActive(true);
        WinGameAudio();
    }

    public void UnlockDoorAudio() {
        audioSource.PlayOneShot(doorUnlockAudio);
    }

    public void TapSpreadAudio() {
        audioSource.PlayOneShot(tapSpreadAudio);
    }

    private void WinGameAudio() {
        audioSource.PlayOneShot(winAudio);
    }
}
