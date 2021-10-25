using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : EntityCombat {

    PlayerEntity _player;

    public void AttackIfPossible (float attackMultiplier = 1f) {
        if (canAttack) {
            Attack((int)(baseDamage * attackMultiplier));
        }
    }

    public override void UpdateCombat() {
        base.UpdateCombat();

        if (_player.transform.position.ToVec2().IsWithinDistanceOf(attackPoint.position, attackRange)) {
            AttackIfPossible();
        }
    }

    void Start () {
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerEntity>();
    }

}
