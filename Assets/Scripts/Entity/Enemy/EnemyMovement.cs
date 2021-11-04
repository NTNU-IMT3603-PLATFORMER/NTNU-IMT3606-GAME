using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    const string BOUNDARY_TAG = "Boundary";

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
    Rigidbody2D _enemyBody;
    CharacterController2D _characterController;

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

    /// <summary>
    /// Who should we go after and attack?
    /// </summary>
    public Transform target => PlayerEntity.INSTANCE.transform;

    // Start is called before the first frame update
    void Start() {
        // Gets all the relevant components needed
        _sr = GetComponent<SpriteRenderer>();
        _enemyBody = GetComponent<Rigidbody2D>();
        _characterController = GetComponent<CharacterController2D>();

        _followPlayer = false;
    }

    void FixedUpdate() {
        isAbovePlayer = transform.position.y > target.position.y;
        isToTheRight = transform.position.x > target.position.x;
        isToTheLeft = transform.position.x < target.position.x;
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
        _characterController.Move(true, new Vector2(isToTheRight ? -_speed : _speed, yVelocity), false, false);
    }


    void CheckMovement() {
        // Player to enemy distance check
        if ((Vector2.Distance(transform.position, target.position) < _followDistance) && !_followPlayer && !_patrolMode) {
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
            _characterController.Move(true, new Vector2(_speed, _enemyBody.velocity.y), false, false);
        } else {
            if (!_isGroundEnemy){
                _enemyBody.isKinematic = true;
            }
            _characterController.Move(true, new Vector2(-_speed, _enemyBody.velocity.y), false, false);
        }
    }

}

