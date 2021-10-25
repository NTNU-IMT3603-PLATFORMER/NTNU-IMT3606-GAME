using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    const string BOUNDARY_TAG = "Boundary";
    const string PLAYER_TAG = "Player";

    [Header("Enemy Variables")]
    [SerializeField, Tooltip("The speed of the enemy")]
    float _speed;
    [SerializeField, Tooltip("The distance from the player to the enemy to make it follow the player")]
    float _followDistance;

    [Header("Enemy Properties")]
    [SerializeField, Tooltip("Patrol Mode makes the enemy not follow the player")]
    bool _patrolMode;
    [SerializeField, Tooltip("Is Ground Enemy toggles between grounded and flying enemies")]
    bool _isGroundEnemy;

    SpriteRenderer _sr;
    Transform _target;
    Rigidbody2D _enemyBody;

    bool _moveRight;
    bool _followPlayer;

    /// <summary>
    /// Is the enemy above the player?
    /// Includes an offset
    /// </summary>
    public bool isAbovePlayer { get; private set; }

    /// <summary>
    /// Is the enemy to the right of the player?
    /// Includes an offset
    /// </summary>
    public bool isToTheRight { get; private set; }

    /// <summary>
    /// Is the enemy to the left of the player?
    /// Includes an offset
    /// </summary>
    public bool isToTheLeft { get; private set; }


    // Start is called before the first frame update
    void Start() {
        // Gets all the relevant components needed
        _sr = GetComponent<SpriteRenderer>();
        _target = GameObject.FindGameObjectWithTag(PLAYER_TAG).transform;
        _enemyBody = GetComponent<Rigidbody2D>();

        _followPlayer = false;
    }

    void FixedUpdate() {
        isAbovePlayer = transform.position.y > _target.position.y + 1;
        isToTheRight = transform.position.x > _target.position.x + 1;
        isToTheLeft = transform.position.x + 1 < _target.position.x;
        CheckMovement(); 
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
        // This makes the enemy move towards the player
        // Uses + 1 to make the enemy stop infront of the player
        _enemyBody.velocity = new Vector2(isToTheRight ? -_speed : _speed, yVelocity);

        if ((isToTheRight && transform.localScale.x < 0) || (isToTheLeft && transform.localScale.x > 0)) {
            // Flip enemy
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }


    void CheckMovement() {
        // Player to enemy distance check
        if ((Vector2.Distance(transform.position, _target.position) < _followDistance) && !_followPlayer && !_patrolMode) {
            _followPlayer = true;
        }

        // Logic to check whether the enemy should patrol or follow the player
        _enemyBody.isKinematic = false;
        if (_followPlayer && _isGroundEnemy){
            FollowPlayerMovement(_enemyBody.velocity.y);
        } else if (_followPlayer && !_isGroundEnemy) {
            FollowPlayerMovement(isAbovePlayer ? -_speed : _speed);
        } else if (_moveRight) {
            if (!_isGroundEnemy) {
                _enemyBody.isKinematic = true;
            }
            _enemyBody.velocity = new Vector2(_speed, _enemyBody.velocity.y);
            _sr.flipX = true;
        } else {
            if (!_isGroundEnemy){
                _enemyBody.isKinematic = true;
            }
            _enemyBody.velocity = new Vector2(-_speed, _enemyBody.velocity.y);
            _sr.flipX = false;
        }
    }
}

