using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour{


    [Header("Hearts")]
    [SerializeField]        GameObject _heartsHolder;
    [SerializeField]        Sprite _heart_full;
    [SerializeField]        Sprite _heart_empty;

    [Header("Blood Bar")]
    [SerializeField]        Slider _slider;
    [SerializeField]        PlayerEntity _playerEntity;

    Image[] _hearts;
    Image _fill;

    void Start () {
        _hearts = _heartsHolder.GetComponentsInChildren<Image>();
        _fill = _slider.GetComponentInChildren<Image>();
    }

    void Update() {
        UpdateHealthBar();
        UpdateBloodBar();
    }

    public void UpdateHealthBar() {
        for (int i = 0; i < _hearts.Length; i++) {
            if(i <= PlayerEntity.INSTANCE.health) {
                _hearts[i].sprite = _heart_full;
            } else {
                _hearts[i].sprite = _heart_empty;
            }
        }
    }
    public void UpdateBloodBar() {
        _slider.value = _playerEntity.bloodlevel;
        if (_slider.value < 3) {
            _fill.color = Color.white;
        } else {
            _fill.color = Color.red; 
        }
    }

}
