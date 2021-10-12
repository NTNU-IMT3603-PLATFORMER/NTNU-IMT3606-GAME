using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PB_UnawareState: State<PB_Data> {

    [SerializeField, Tooltip("The distance at which player will be noticed")] float _distanceToNoticePlayer = 5f;

    public override void OnUpdateState() {
        // Checks if player is within sight
        if (data.player.transform.position.IsWithinDistanceOf(transform.position, _distanceToNoticePlayer)) {
            fsm.ChangeState<PB_AttackState>();
        }
    }

    public override void OnFixedUpdateState() {
        // Make sure we stand still
        data.characterController2D.Move(false, Vector2.zero, false, false);
    }

}
