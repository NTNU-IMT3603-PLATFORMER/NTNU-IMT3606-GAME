using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PB_AttackState : State {

    public override void OnEnterState() {
        Debug.Log("Enter state");

        fsm.ChangeState<PB_TiredState>();
    }

    public override void OnExitState() {
        Debug.Log("Exit state");
    }

    public override void OnUpdateState() {
        Debug.Log("Update state");
    }

}
