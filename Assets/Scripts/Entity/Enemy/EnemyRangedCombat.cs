using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedCombat : EnemyCombat {

    [SerializeField]    GameObject _prefabRangedAttack;

    /// <summary>
    /// Ranged attack
    /// </summary>
    /// <param name="damage">The damage the attack does</param>
    public override void Attack(int damage) {
        // Instantiate a ranged attack at the attack position
        GameObject rangedAttack = Instantiate<GameObject>(_prefabRangedAttack, attackPoint.position, Quaternion.identity);

        timeLeftToAllowAttack = attackCooldown;
    }
}
