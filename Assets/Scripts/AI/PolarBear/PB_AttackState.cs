using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PB_AttackState : State<PB_Data> {

    bool _charge;
    bool _shockWave;
    Vector2 _move;
    AttackStrategy _currentAttackStrategy;

    enum AttackStrategy {
        NormalAttack,
        Charge,
        Shockwave
    }

    // The time before the AI is allowed to change strategy
    float _strategyDecisionCountdown;

    public override void OnEnterState() {

    }

    public override void OnExitState() {

    }

    public override void OnUpdateState() {
        _strategyDecisionCountdown -= Time.deltaTime;

        if (_strategyDecisionCountdown <= 0f) {
            UpdateStrategy();
            _strategyDecisionCountdown = Random.Range(1f, 5f);

            Debug.Log(_currentAttackStrategy);
        }

        // Are we within attacking distance to the player?
        if (data.player.transform.position.IsWithinDistanceOf(transform.position, 1f)) {
            _move = Vector2.zero;

            if (_currentAttackStrategy == AttackStrategy.NormalAttack) {
                // TODO: Perform normal attack here
            }
        } else {
            Vector2 moveDirection = (data.player.transform.position - transform.position).normalized;
            moveDirection.y = 0f;

            _move = moveDirection * data.speed;
        }

        // Are we within charging distance?
        if (data.player.transform.position.IsWithinDistanceOf(transform.position, 8f) && _currentAttackStrategy == AttackStrategy.Charge) {
            _charge = true;
        } else {
            _charge = false;
        }
    }

    public override void OnFixedUpdateState() {
        data.characterController2D.Move(_move != Vector2.zero, _move, false, _charge);

        if (_charge) {
            UpdateStrategy();
        }
    }

    void UpdateStrategy () {
        if (Random.value < 0.33f) {
            _currentAttackStrategy = AttackStrategy.Charge;
        } else if (Random.value < 0.33f) {
            _currentAttackStrategy = AttackStrategy.Shockwave;
        } else {
            _currentAttackStrategy = AttackStrategy.NormalAttack;
        }
    }

}
