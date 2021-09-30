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
        if (_followPlayer) {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
            //Vector2 moveTowards = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.fixedDeltaTime);
            //_enemyBody.MovePosition(new Vector2(moveTowards.x, transform.position.y));
            if (transform.position.x > _target.position.x) {
               _sr.flipX = false;
            } else {
               _sr.flipX = true;
            }
        } else if (_moveRight) {
            transform.Translate(_speed * Time.deltaTime * _speed, 0, 0);
            _sr.flipX = true;
        } else { 
            transform.Translate(-_speed * Time.deltaTime * _speed, 0, 0);
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
}
