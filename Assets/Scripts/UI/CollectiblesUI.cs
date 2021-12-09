using TMPro;
using UnityEngine;

/// <summary>
/// This scripts updates the UI when a collectible is picked up. 
/// </summary>
public class CollectiblesUI : MonoBehaviour {

    TextMeshProUGUI _text;

    public PlayerCollectable playerCollectible => PlayerEntity.INSTANCE.GetComponent<PlayerCollectable>();

    void Start() {
        _text = GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        _text.text = playerCollectible.orbsCollected.ToString();
    }

}