using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Creates a fading "animation" to transition into the next scene when defeating a boss. 
/// </summary>
public class FadePanel : MonoBehaviour {

    [SerializeField]
    float _fadeTime = 4f;
    [SerializeField, Range(0f, 1f)]
    float _fadeTarget;

    Image _image;

    /// <summary>
    /// Controls fade target for black screen. 
    /// Target of 0 is no black screen. 
    /// Target of 1 is full black screen
    /// </summary>
    public float fadeTarget {
        get => _fadeTarget;
        set => _fadeTarget = value;
    }

    void Start () {
        _image = GetComponent<Image>();
        
        // Start scene by being completely black
        Color newColor = _image.color;
        newColor.a = 1f;
        _image.color = newColor;
    }

    void Update () {
        // Gradually fade towards fade target based on fade time
        Color newColor = _image.color;
        newColor.a = Mathf.MoveTowards(newColor.a, _fadeTarget, 1f / _fadeTime * Time.deltaTime);
        _image.color = newColor;
    }

}