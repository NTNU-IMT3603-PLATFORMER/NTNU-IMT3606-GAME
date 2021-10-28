using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : EntityCombat {
    
    public override void UpdateCombat() {
        base.UpdateCombat();

        if (Input.GetButtonDown("Attack") && canAttack) {
            Attack(baseDamage);
            AudioManager.instance.PlaySound("swordhit");
        }
    }

}