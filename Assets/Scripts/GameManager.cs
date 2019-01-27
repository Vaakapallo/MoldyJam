using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public MoldType chosenType = MoldType.Pink;
    public int clicksLeft = 5;
    public Slider timeScaleSlider;
    public GameObject nextLevelButton;
    public GameObject restartButton;
    public Text clicksText;
    public AudioClip doorUnlockAudio;
    public AudioClip tapSpreadAudio;
    public AudioClip winAudio;

    public List<Tile> tiles;
    private AudioSource audioSource;

    public static GameManager instance;

    [Range(1,50)]
    public float timeScale = 1.0f;

    void Awake(){
        instance = this;
        tiles = FindObjectsOfType<Tile>().ToList();
        timeScaleSlider.onValueChanged.AddListener(val => ChangeTimescale(val));
        audioSource = GetComponent<AudioSource>();
        if(clicksText != null)
            clicksText.text = "Infections Left: " + clicksLeft.ToString();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.S)) {
            LoadNextLevel();
        }
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

    public void DecreaseClicks() {
        clicksLeft--;
        clicksText.text = "Infections Left: " + clicksLeft.ToString();
        if(clicksLeft == 0) {
            restartButton.SetActive(true);
        }
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
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
