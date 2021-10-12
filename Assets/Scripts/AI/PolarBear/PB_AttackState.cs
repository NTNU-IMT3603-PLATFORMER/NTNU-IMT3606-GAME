using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PB_AttackState : State<PB_Data> {

    public override void OnEnterState() {
        Debug.Log("Enter state");
    }

    public override void OnExitState() {
        Debug.Log("Exit state");
    }

    public override void OnUpdateState() {
        Debug.Log("Update state");
    }

}
