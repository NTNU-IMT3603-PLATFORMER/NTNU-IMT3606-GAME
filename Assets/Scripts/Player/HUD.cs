using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour{

    [SerializeField]
    GameObject _heartsHolder;

    [SerializeField]
    Sprite _heart_full;

    [SerializeField]
    Sprite _heart_empty;

    PlayerEntity _playerEntity;
    Image[] _hearts;

    void Start () {
        _hearts = _heartsHolder.GetComponentsInChildren<Image>();
        _playerEntity = GameObject.FindWithTag("Player").GetComponent<PlayerEntity>();
    }

    void Update() {
        for (int i = 0; i < _hearts.Length; i++) {
            if(i <= _playerEntity.health) {
                _hearts[i].sprite = _heart_full;
            } else {
                _hearts[i].sprite = _heart_empty;
            }
        }
    }

}
