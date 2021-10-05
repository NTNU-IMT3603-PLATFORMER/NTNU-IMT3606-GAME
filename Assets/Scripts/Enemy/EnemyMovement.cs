using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    const string BOUNDARY_TAG = "Boundary";

    [SerializeField, Tooltip("The speed of the enemy")]
    float _speed;
    [SerializeField, Tooltip("Enable patrol only mode")]
    bool _patrolMode;
    [SerializeField, Tooltip("The distance from the player to the enemy to make it follow the player")]
    float _followDistance;
    [SerializeField, Tooltip("Is the enemy a ground enemy or a flying enemy?")]
    bool _isGroundEnemy;

    SpriteRenderer _sr;
    Transform _target;
    Rigidbody2D _enemyBody;

    bool _moveRight;
    bool _followPlayer;


    // Start is called before the first frame update
    void Start() {
        // Gets all the relevant components needed
        _sr = GetComponent<SpriteRenderer>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _enemyBody = GetComponent<Rigidbody2D>();

        _followPlayer = false;
    }

    // Update is called once per frame
    void FixedUpdate() {
        // Player to enemy distance check
        if ((Vector2.Distance(transform.position, _target.position) < _followDistance) && !_followPlayer && !_patrolMode) {
            _followPlayer = true;
        }
        
        // Enemy follow or patrol logic
        if (_followPlayer && _isGroundEnemy) {
            Movement(_enemyBody.velocity.y);
        } else if (_followPlayer && !_isGroundEnemy) {
           if (transform.position.y > _target.position.y + 1.3) {
                Movement(-_speed);
            } else {
                Movement(_speed);
            }
        } else if (_moveRight) {
            _enemyBody.velocity = new Vector2(_speed, _enemyBody.velocity.y);
            _sr.flipX = true;
        } else {
            _enemyBody.velocity = new Vector2(-_speed, _enemyBody.velocity.y);
            _sr.flipX = false;
        }
    }

    void OnTriggerEnter2D(Collider2D trig) {
        // Check if enemy has entered a boundary
        if (trig.gameObject.CompareTag(BOUNDARY_TAG)) {
            if (_moveRight) {
                _moveRight = false;
            } else {
                _moveRight = true;
            }
        }
    }

    void Movement(float yVelocity) {
        if (_followPlayer) {
            if (transform.position.x > _target.position.x) {
               _sr.flipX = false;
               _enemyBody.velocity = new Vector2(-_speed, yVelocity);
            } else {
               _sr.flipX = true;
               _enemyBody.velocity = new Vector2(_speed, yVelocity);
            }
        }    
    }
}
