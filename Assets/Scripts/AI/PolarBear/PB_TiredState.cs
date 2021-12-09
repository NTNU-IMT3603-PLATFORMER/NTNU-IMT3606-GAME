using System.Collections;
using UnityEngine;

public class PB_TiredState : State<PB_Data> {

    [SerializeField, Tooltip("Time to be tired before being able to attack again")]
    float _waitTime = 4f;

    public override void OnEnterState() {
        StartCoroutine(Wait());
    }

    public override void OnFixedUpdateState() {
        // Make sure we stand still
        data.characterController2D.Move(false, Vector2.zero, false, false);
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(_waitTime);
        fsm.ChangeState<PB_AttackState>();
    }

}
