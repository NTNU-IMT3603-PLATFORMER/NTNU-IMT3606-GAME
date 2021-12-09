using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Contains the main menu functions for starting and quitting the game.
/// </summary>
public class MainMenu : MonoBehaviour {

    const string FIRST_LEVEL_SCENE = "Ice_Level_1";

    [SerializeField, Tooltip("The options menu panel")]
    Transform _optionsMenu;

    /// <summary>
    /// Loads the first level.
    /// </summary>
    public void PlayGame() {
        SceneManager.LoadScene(FIRST_LEVEL_SCENE);
    }


    /// <summary>
    /// Closes the game.
    /// </summary>
    public void QuitGame() {
        Debug.Log("quitted");
        Application.Quit();
    }

    void Start() {
        // Initialize options to load and apply saved values
        foreach (IOption option in _optionsMenu.GetComponentsInChildren<IOption>(true)) {
            option.InitializeOption();
        }
    }

}
