using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : EntityCombat {

    float _timeLeftToAllowAttack;

    public void AttackIfPossible (float attackMultiplier = 1f) {
        if (_timeLeftToAllowAttack <= 0f) {
            Attack((int)(baseDamage * attackMultiplier));
            _timeLeftToAllowAttack = attackRate;
        }
    }

    void Update () {
        if (_timeLeftToAllowAttack > 0) {
            _timeLeftToAllowAttack -= Time.deltaTime; 
        }
    }

}
