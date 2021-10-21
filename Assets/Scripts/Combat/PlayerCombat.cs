using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : EntityCombat {

    Entity _player;
    float _nextAttackTime;

    void Start () {
        _player = GetComponent<Entity>();
    }

    // Update is called once per frame
    void Update() {
        if (_nextAttackTime <= 0) {
            if (Input.GetButtonDown("Attack")) {
                Attack(baseDamage);
                _nextAttackTime = attackRate;
            }
        } else {
            _nextAttackTime -= Time.deltaTime; 
        }
    }

}
    