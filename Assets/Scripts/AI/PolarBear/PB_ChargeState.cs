using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PB_ChargeState : State<PB_Data> {

    [SerializeField, Tooltip("Percentage chance to get tired after each special attack")] float _chanceToGetTired = 0.2f;

    public override void OnEnterState() {
        // Trigger charge in direction of player

        Vector2 moveDirection = (data.player.transform.position - transform.position).normalized;

        data.characterController2D.Move(true, moveDirection, false, true);
        data.enemyEntity.invincible = true;
    }

    public override void OnFixedUpdateState() {
        if (data.characterController2D.isDashing) {
            data.characterController2D.Move(false, Vector2.zero, false, true);
        } else {
            // "Roll dice" to see if we should be tired
            if (Random.value < _chanceToGetTired) {
                fsm.ChangeState<PB_TiredState>();
            } else {
                fsm.ChangeState<PB_AttackState>();
            }
        }
    }

    public override void OnExitState() {
        data.enemyEntity.invincible = false;
    }

}