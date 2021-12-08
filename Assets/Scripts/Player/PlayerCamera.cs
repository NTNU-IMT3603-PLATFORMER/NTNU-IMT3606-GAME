using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerCamera makes the camera follow the player.
/// </summary>
public class PlayerCamera : MonoBehaviour {

    [SerializeField, Tooltip("Smooth time used with SmoothDamp."), Range(0, 1)]
    float _smoothFactor = 0.1f;
    [SerializeField, Tooltip("Target to follow (player)")]
    Transform _target;

    Vector3 _targetPosition;
    Vector3 _positionV;

    void Update() {
        _targetPosition = new Vector3(_target.position.x, _target.position.y, -10f);

        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _positionV, _smoothFactor);
    }

}
