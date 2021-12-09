using UnityEngine;

/// <summary>
/// Responsible for player combat
/// </summary>
public class PlayerCombat : EntityCombat {

    public override void UpdateCombat() {
        base.UpdateCombat();

        if (Input.GetButtonDown("Attack") && canAttack) {
            Attack(baseDamage);
            AudioManager.instance.PlaySound("attack");
        }
    }

}