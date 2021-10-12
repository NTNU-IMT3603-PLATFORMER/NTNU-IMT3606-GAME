using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PB_ChargeState : State<PB_Data> {

    public override void OnEnterState() {
        // Trigger charge in direction of player

        Vector2 moveDirection = (data.player.transform.position - transform.position).normalized;

        data.characterController2D.Move(true, moveDirection, false, true);
    }

    public override void OnExitState() {}
    public override void OnUpdateState() {}

    public override void OnFixedUpdateState() {
        if (data.characterController2D.isDashing) {
            data.characterController2D.Move(false, Vector2.zero, false, true);
        } else {
            fsm.ChangeState<PB_AttackState>();
        }
    }

}