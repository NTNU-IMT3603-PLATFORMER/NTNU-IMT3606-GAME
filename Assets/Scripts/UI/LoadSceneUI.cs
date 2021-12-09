using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// This script is responsible for loading a specific scene. 
/// </summary>
public class LoadSceneUI : MonoBehaviour {

    /// <summary>
    /// Loads a given scene. 
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }


}