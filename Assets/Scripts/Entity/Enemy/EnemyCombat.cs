using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : EntityCombat {

    /// <summary>
    /// Attack if cooldown has reached 0
    /// </summary>
    public void AttackIfPossible (float attackMultiplier = 1f) {
        if (canAttack) {
            Attack((int)(baseDamage * attackMultiplier));
            AudioManager.instance.PlaySound("attack");
        }
    }

    public override void UpdateCombat() {
        base.UpdateCombat();

        if (PlayerEntity.INSTANCE.transform.position.ToVec2().IsWithinDistanceOf(attackPoint.position, attackRange)) {
            AttackIfPossible();
        }
    }

}
