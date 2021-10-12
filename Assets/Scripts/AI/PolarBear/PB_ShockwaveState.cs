using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PB_ShockwaveState : State<PB_Data> {

    [SerializeField, Tooltip("Percentage chance to get tired after each special attack")] float _chanceToGetTired = 0.5f;

    Phase _phase;

    enum Phase {
        Jump,
        SlamIntoGround,
        Wait
    }

    public override void OnEnterState() {
        _phase = Phase.Jump;

        StartCoroutine(Shockwave());
    }

    public override void OnFixedUpdateState() {
        switch (_phase) {
            case Phase.Jump:
                data.characterController2D.Move(false, Vector2.zero, true, false);
                break;
            case Phase.SlamIntoGround:
                data.characterController2D.rigidbody.velocity = Physics.gravity * 4f;
                
                // Call Move to update controller state (e.g. isGrounded)
                data.characterController2D.Move(false, Vector2.zero, false, false);
                break;
            case Phase.Wait:
                // Should stay still while waiting
                data.characterController2D.Move(false, Vector2.zero, false, false);
                break;
        }
    }

    IEnumerator Shockwave () {
        // Wait for jump to reach some height
        yield return new WaitForSeconds(0.5f);

        // Then slam into ground
        _phase = Phase.SlamIntoGround;

        // Wait until we hit ground
        yield return new WaitUntil(() => data.characterController2D.isGrounded);

        // TODO: Spawn shockwave here

        // Wait a little bit to catch our breath
        _phase = Phase.Wait;
        yield return new WaitForSeconds(1f);

        // "Roll dice" to see if we should be tired
        if (Random.value < _chanceToGetTired) {
            fsm.ChangeState<PB_TiredState>();
        } else {
            fsm.ChangeState<PB_AttackState>();
        }
    }

}