using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpiritsUI : MonoBehaviour {
    
    TextMeshProUGUI _text;

    void Start () {
        _text = GetComponent<TextMeshProUGUI>();
    }    

    void Update() {
        _text.text = PlayerEntity.INSTANCE.spirits.ToString();
    }
    
}
