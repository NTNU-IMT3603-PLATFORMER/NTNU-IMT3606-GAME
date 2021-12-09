using UnityEngine;

/// <summary>
/// Contains a simple function
/// for destroying player object
/// Used when exiting from credits
/// to main menu
/// </summary>
public class DestroyPlayer : MonoBehaviour {

    /// <summary>
    /// Destroy player game object
    /// </summary>
    public void DestroyPlayerObject() {
        Destroy(PlayerEntity.INSTANCE.gameObject);
    }

}