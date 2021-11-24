using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedCombat : EnemyCombat {


    public override void Attack(int damage) {

        

        timeLeftToAllowAttack = attackCooldown;
    }
}
