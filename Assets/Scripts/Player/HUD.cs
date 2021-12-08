using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HUD renders the health and the blood bar in the game HUD.
/// </summary>
public class HUD : MonoBehaviour {

    [Header("Hearts")]
    [SerializeField]
    GameObject _heartsHolder;
    [SerializeField]
    Sprite _heart_full;
    [SerializeField]
    Sprite _heart_empty;

    [Header("Blood Bar")]
    [SerializeField]
    Slider _slider;

    Image[] _hearts;
    Image _fill;

    /// <summary>
    /// UpdateHealthBar fills the heart bar in the HUD for each heart the player has.
    /// </summary>
    public void UpdateHealthBar() {
        for (int i = 0; i < _hearts.Length; i++) {
            if (i <= PlayerEntity.INSTANCE.health) {
                _hearts[i].sprite = _heart_full;
            } else {
                _hearts[i].sprite = _heart_empty;
            }
        }
    }

    /// <summary>
    /// UpdateBloodBar fills the blood bar in the HUD.
    /// If the of the bar is less than 3, it is white, else it is red.
    /// </summary>
    public void UpdateBloodBar() {
        _slider.value = PlayerEntity.INSTANCE.bloodlevel;
        if (_slider.value < 3) {
            _fill.color = Color.white;
        } else {
            _fill.color = Color.red;
        }
    }

    void Start() {
        _hearts = _heartsHolder.GetComponentsInChildren<Image>();
        _fill = _slider.GetComponentInChildren<Image>();
    }

    void Update() {
        UpdateHealthBar();
        UpdateBloodBar();
    }

}
