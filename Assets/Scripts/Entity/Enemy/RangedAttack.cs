using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour {

    Transform _player;
    Vector3 _movementDirection;
    float _timeLeft = 2f;
    
   // Start is called before the first frame update
    void Start() {
        _player = PlayerEntity.INSTANCE.transform;
        if(transform.position.x > _player.position.x) {
            _movementDirection = new Vector3(-1, 0, 0); 
        } else {
            _movementDirection = new Vector3(1, 0, 0); 
        }
    }

    // Update is called once per frame
    void Update() {
        if(_timeLeft > 0) {
            transform.Translate(_movementDirection * Time.deltaTime * 5);
            _timeLeft -= Time.deltaTime;
        } else {
            Destroy(gameObject);
        } 
    }
}
