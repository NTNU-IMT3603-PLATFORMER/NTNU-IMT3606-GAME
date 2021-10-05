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

        CheckMovement();

        // Enemy follow or patrol logic
      
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

    void FollowPlayerMovement(float yVelocity) {
        if (_followPlayer) {
            // Uses + 1 to make the enemy stop infront of the player
            if (transform.position.x > _target.position.x + 1) {
               _sr.flipX = false;
               _enemyBody.velocity = new Vector2(-_speed, yVelocity);
            } else  if (transform.position.x + 1 < _target.position.x){
               _sr.flipX = true;
               _enemyBody.velocity = new Vector2(_speed, yVelocity);
            }
        } 
    }


    void CheckMovement() {
        _enemyBody.isKinematic = false;
        if (_followPlayer && _isGroundEnemy)
        {
            FollowPlayerMovement(_enemyBody.velocity.y);
        }
        else if (_followPlayer && !_isGroundEnemy)
        {

            if (transform.position.y > _target.position.y + 1)
            {
                FollowPlayerMovement(-_speed);
            }
            else
            {
                FollowPlayerMovement(_speed);
            }
        }
        else if (_moveRight)
        {
            if (!_isGroundEnemy) {
                _enemyBody.isKinematic = true;
            }
            _enemyBody.velocity = new Vector2(_speed, _enemyBody.velocity.y);
            _sr.flipX = true;
        }
        else
        {
            if (!_isGroundEnemy){
                _enemyBody.isKinematic = true;
            }
            _enemyBody.velocity = new Vector2(-_speed, _enemyBody.velocity.y);
            _sr.flipX = false;
        }
    }
}

