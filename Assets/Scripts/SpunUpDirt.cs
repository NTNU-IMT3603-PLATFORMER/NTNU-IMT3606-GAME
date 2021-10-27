using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpunUpDirt : MonoBehaviour {

    [SerializeField, Tooltip("Damage inflicted on player on hit")]          int _moveSpeed = 5;
    [SerializeField, Tooltip("Damage inflicted on player on hit")]          int _damage = 10;
    [SerializeField, Tooltip("Cooldown for inflicting damage to player")]   float _damageCooldown = 0.5f;
    [SerializeField, Tooltip("Radius for inflicting damage")]               float _damageRadius = 0.8f;

    PlayerEntity _playerEntity => PlayerEntity.INSTANCE;
    float _timeLeftToInflictDamage;
    bool _moveLeft;

    /// <summary>
    /// Should the dirt move left? (or right?)
    /// </summary>
    public bool moveLeft {
        get => _moveLeft;
        set => _moveLeft = value;
    }

    void Update () {
        if (_timeLeftToInflictDamage <= 0f) {
            if (_playerEntity.transform.position.IsWithinDistanceOf(transform.position, _damageRadius)){
                _playerEntity.InflictDamage(_damage, transform, 10f);
                _timeLeftToInflictDamage = _damageCooldown;
            }
        } else {
            _timeLeftToInflictDamage -= Time.deltaTime;
        }

        Vector3 direction = moveLeft ? Vector3.left : Vector3.right;
        transform.Translate(direction * _moveSpeed * Time.deltaTime);
    }

}
