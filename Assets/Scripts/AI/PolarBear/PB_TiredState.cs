using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PB_TiredState : State {

    public override void OnEnterState() {
        Debug.Log("Enter tired");
    }

    public override void OnExitState() {
        Debug.Log("Exit tired");
    }

    public override void OnUpdateState() {
        Debug.Log("Update tired");

        fsm.ChangeState<PB_AttackState>();
    }

}
