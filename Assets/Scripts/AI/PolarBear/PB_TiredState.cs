using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PB_TiredState : State {

    public override void OnUpdateState() {
        fsm.ChangeState<PB_AttackState>();
    }

}
