using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : EntityCombat {

    public void AttackIfPossible (float attackMultiplier = 1f) {
        if (canAttack) {
            Attack((int)(baseDamage * attackMultiplier));
        }
    }

}
