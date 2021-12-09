using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the moving platform. 
/// </summary>
public class MovingPlatform : MonoBehaviour {

    [SerializeField]
    Rigidbody2D _rb;
    [SerializeField]
    List<Transform> _waypoints;
    [SerializeField]
    float _moveSpeed;
    [SerializeField]
    int _target;

    float _distanceLeft;

    /// <summary>
    /// Rigid body attached to moving platform. 
    /// </summary>
    public Rigidbody2D rb => _rb;

    private void FixedUpdate() {
        if (_distanceLeft > 0f) {
            _distanceLeft -= _moveSpeed * Time.fixedDeltaTime;
        }

        Vector2 direction = (_waypoints[_target].position.ToVec2() - rb.position).normalized;
        rb.velocity = _moveSpeed * direction;

        //use data
        if (_distanceLeft <= 0f) {
            if (_target == _waypoints.Count - 1) {
                _target = 0;
            } else {
                _target += 1;
            }

            _distanceLeft = (_waypoints[_target].position.ToVec2() - rb.position).magnitude;
        }
    }

}
