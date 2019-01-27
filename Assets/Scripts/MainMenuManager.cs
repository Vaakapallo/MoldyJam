using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public static MainMenuManager Instance;

    void Awake() {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Quit() {
        Application.Quit();
    }

    public void StartGame() {
        SceneManager.LoadScene("Level1");
    }

    public void LoadNextLevel() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
}
