using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level_Tutorial");
    }

    public void QuitGame()
    {
        Debug.Log("quitted");
        Application.Quit();
    }
}
