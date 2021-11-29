using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    const string FIRST_LEVEL_SCENE = "Ice_Level_1";

    [SerializeField, Tooltip("The options menu panel")]  Transform _optionsMenu;

    public void PlayGame() {
        SceneManager.LoadScene(FIRST_LEVEL_SCENE);
    }

    public void QuitGame() {
        Debug.Log("quitted");
        Application.Quit();
    }

    void Start () {
        // Initialize options to load and apply saved values
        foreach (IOption option in _optionsMenu.GetComponentsInChildren<IOption>(true)) {
            option.InitializeOption();
        }
    }

}
