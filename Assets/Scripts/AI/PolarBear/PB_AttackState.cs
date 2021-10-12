using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PB_AttackState : State<PB_Data> {

    [SerializeField, Tooltip("Higher weight will mean higher chance of normal attack")]     int _normalAttackWeight = 4;
    [SerializeField, Tooltip("Higher weight will mean higher chance of charge attack")]     int _chargeAttackWeight = 2;
    [SerializeField, Tooltip("Higher weight will mean higher chance of shockwave attack")]  int _shockwaveAttackWeight = 1;

    Vector2 _move;

    // Mappings to the weights set in inspector
    // Allows for realtime editing
    Dictionary<PB_Data.AttackStrategy, int> attackWeights => new Dictionary<PB_Data.AttackStrategy, int>() {
        [PB_Data.AttackStrategy.NormalAttack] = _normalAttackWeight,
        [PB_Data.AttackStrategy.Charge] = _chargeAttackWeight,
        [PB_Data.AttackStrategy.Shockwave] = _shockwaveAttackWeight,
    };

    public override void OnEnterState() {
        UpdateStrategy();
    }

    public override void OnExitState() {}

    public override void OnUpdateState() {
        data.strategyDecisionCountdown -= Time.deltaTime;

        if (data.strategyDecisionCountdown <= 0f) {
            UpdateStrategy();
            data.strategyDecisionCountdown = Random.Range(1f, 5f);
        }

        // Are we within attacking distance to the player?
        if (data.player.transform.position.IsWithinDistanceOf(transform.position, 1f)) {
            _move = Vector2.zero;

            if (data.currentAttackStrategy == PB_Data.AttackStrategy.NormalAttack) {
                // TODO: Perform normal attack here
            }
        } else {
            Vector2 moveDirection = (data.player.transform.position - transform.position).normalized;
            moveDirection.y = 0f;

            _move = moveDirection * data.speed;
        }
        
        // Should we charge or perform shockwave attack?
        if (data.player.transform.position.IsWithinDistanceOf(transform.position, 8f) && data.currentAttackStrategy == PB_Data.AttackStrategy.Charge) {
            fsm.ChangeState<PB_ChargeState>();
        } else if (data.player.transform.position.IsWithinDistanceOf(transform.position, 10f) && data.currentAttackStrategy == PB_Data.AttackStrategy.Shockwave && data.characterController2D.canJumpFromGroundOrWall) {
            fsm.ChangeState<PB_ShockwaveState>();
        }
    }

    public override void OnFixedUpdateState() {
        data.characterController2D.Move(_move != Vector2.zero, _move, false, false);
    }

    void UpdateStrategy () {
        int randomNumber = Random.Range(0, attackWeights.Values.Sum());
        int currentSum = 0;

        foreach (KeyValuePair<PB_Data.AttackStrategy, int> weightPair in attackWeights) {
            // Check if random number is within range for the current attack strategy
            if (randomNumber < weightPair.Value + currentSum) {
                data.currentAttackStrategy = weightPair.Key;
                return;
            }

            currentSum += weightPair.Value;
        }
    }

}
