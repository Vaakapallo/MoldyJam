using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public void Quit() {
        Application.Quit();
    }

    public void StartGame() {
        SceneManager.LoadScene("Level1");
    }

    
}
