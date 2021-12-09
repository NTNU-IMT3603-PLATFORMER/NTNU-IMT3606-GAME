using TMPro;
using UnityEngine;

/// <summary>
/// Updates the UI when picking up a spirit. 
/// </summary>
public class SpiritsUI : MonoBehaviour {

    TextMeshProUGUI _text;

    void Start() {
        _text = GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        _text.text = PlayerEntity.INSTANCE.spirits.ToString();
    }

}
