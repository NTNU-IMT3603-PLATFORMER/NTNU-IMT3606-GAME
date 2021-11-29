using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour {

    [SerializeField]                    float _fadeTime = 4f;
    [SerializeField, Range(0f, 1f)]     float _fadeTarget;

    Image _image;

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