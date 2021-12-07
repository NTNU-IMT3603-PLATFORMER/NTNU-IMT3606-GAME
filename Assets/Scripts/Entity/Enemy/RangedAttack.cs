using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour {

    Transform _player;
    Rigidbody2D _rigidbody;
    float _timeLeft = 2f;

    float _projectileSpeed;

    public float projectileSpeed {
        get => _projectileSpeed;
        set => _projectileSpeed = value;
    }

    public float timeLeft {
        get => _timeLeft;
        set => _timeLeft = value;
    }

    void Start() {
        _player = PlayerEntity.INSTANCE.transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = (_player.position - transform.position).normalized * _projectileSpeed;
    }

    // Update is called once per frame
    void Update() {
        if (_timeLeft > 0) {
            _timeLeft -= Time.deltaTime;
        } else {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<Entity>().InflictDamage(1, transform.position, 5);
        } else if(collision.tag != "Boundary"){
            Destroy(gameObject);
        }
    }
    
}
