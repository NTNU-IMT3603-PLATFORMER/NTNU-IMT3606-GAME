using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PB_AttackState : State<PB_Data> {

    Vector2 _move;

    public override void OnEnterState() {
        UpdateStrategy();
    }

    public override void OnExitState() {

    }

    public override void OnUpdateState() {
        data.strategyDecisionCountdown -= Time.deltaTime;

        if (data.strategyDecisionCountdown <= 0f) {
            UpdateStrategy();
            data.strategyDecisionCountdown = Random.Range(1f, 5f);
        }

        // Are we within attacking distance to the player?
        if (data.player.transform.position.IsWithinDistanceOf(transform.position, 1f)) {
            _move = Vector2.zero;

            if (data.currentAttackStrategy == PB_Data.AttackStrategy.NormalAttack) {
                // TODO: Perform normal attack here
            }
        } else {
            Vector2 moveDirection = (data.player.transform.position - transform.position).normalized;
            moveDirection.y = 0f;

            _move = moveDirection * data.speed;
        }

        // Should we charge?
        if (data.player.transform.position.IsWithinDistanceOf(transform.position, 8f) && data.currentAttackStrategy == PB_Data.AttackStrategy.Charge) {
            fsm.ChangeState<PB_ChargeState>();
        }
    }

    public override void OnFixedUpdateState() {
        data.characterController2D.Move(_move != Vector2.zero, _move, false, false);
    }

    void UpdateStrategy () {
        if (Random.value < 0.33f) {
            data.currentAttackStrategy = PB_Data.AttackStrategy.Charge;
        } else if (Random.value < 0.33f) {
            data.currentAttackStrategy = PB_Data.AttackStrategy.Shockwave;
        } else {
            data.currentAttackStrategy = PB_Data.AttackStrategy.NormalAttack;
        }
    }

}
