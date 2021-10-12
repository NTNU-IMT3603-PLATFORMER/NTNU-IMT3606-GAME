using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PB_AttackState : State<PB_Data> {

    Vector2 _move;

    public override void OnEnterState() {

    }

    public override void OnExitState() {

    }

    public override void OnUpdateState() {
        // Are we close to the player?
        if (data.player.transform.position.IsWithinDistanceOf(transform.position, 1f)) {
            _move = Vector2.zero;
        } else {
            Vector2 moveDirection = (data.player.transform.position - transform.position).normalized;
            moveDirection.y = 0f;

            _move = moveDirection * data.speed;
        }
    }

    public override void OnFixedUpdateState() {
        

        data.characterController2D.Move(_move != Vector2.zero, _move, false, false);
    }

}
