using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectiblesUI : MonoBehaviour {

    TextMeshProUGUI _text;
    
    public PlayerCollectable playerCollectible => PlayerEntity.INSTANCE.GetComponent<PlayerCollectable>();

    void Start () {
        _text = GetComponent<TextMeshProUGUI>();
    }    

    void Update() {
        _text.text = playerCollectible.orbsCollected.ToString();
    }

}