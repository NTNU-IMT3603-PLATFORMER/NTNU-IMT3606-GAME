using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour {

    Transform _player;
    Rigidbody2D _rigidbody;
    float _timeLeft = 2f;


    // Start is called before the first frame update
    void Start() {
        _player = PlayerEntity.INSTANCE.transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = (_player.position - transform.position).normalized * 5;
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
        } else {
            Destroy(gameObject);
        }
    }
}
