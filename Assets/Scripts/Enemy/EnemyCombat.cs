using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : EntityCombat {

    [SerializeField, Tooltip("How often the enemy can attack")]
    float _attackRate;

    EnemyEntity _enemy;

    float _timeLeftToAllowAttack;

    public void AttackIfPossible (float attackMultiplier = 1f) {
        if (_timeLeftToAllowAttack <= 0f) {
            Attack((int)(_enemy.baseDamage * attackMultiplier));
            _timeLeftToAllowAttack = _attackRate;
        }
    }

    void Start () {
        _enemy = GetComponent<EnemyEntity>();
    }

    void Update () {
        if (_timeLeftToAllowAttack > 0) {
            _timeLeftToAllowAttack -= Time.deltaTime; 
        }
    }

}
