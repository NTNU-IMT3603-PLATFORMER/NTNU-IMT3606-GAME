using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PB_AttackState : State<PB_Data> {

    [SerializeField, Tooltip("Higher weight will mean higher chance of normal attack")]
    int _normalAttackWeight = 4;
    [SerializeField, Tooltip("Higher weight will mean higher chance of charge attack")]
    int _chargeAttackWeight = 2;
    [SerializeField, Tooltip("Higher weight will mean higher chance of shockwave attack")]
    int _shockwaveAttackWeight = 1;
    [SerializeField, Tooltip("Min (x) and Max (y) random time interval for changing strategy")]
    Vector2 _randomizedChangeStrategyTime = new Vector2(1f, 5f);

    // The time before the AI is allowed to change strategy
    float _strategyDecisionCountdown;

    EnemyCombat _enemyCombat;
    float _attackTimeLeft;
    Vector2 _move;

    // Mappings to the weights set in inspector
    // Allows for realtime editing
    Dictionary<PB_Data.AttackStrategy, int> attackWeights => new Dictionary<PB_Data.AttackStrategy, int>() {
        [PB_Data.AttackStrategy.NormalAttack] = _normalAttackWeight,
        [PB_Data.AttackStrategy.Charge] = _chargeAttackWeight,
        [PB_Data.AttackStrategy.Shockwave] = _shockwaveAttackWeight,
    };

    bool isWithinNormalAttackRange => data.player.transform.position.IsWithinDistanceOf(transform.position, 1f);
    bool isWithinChargeAttackRange => data.player.transform.position.IsWithinDistanceOf(transform.position, 8f);
    bool isWithinShockwaveAttackRange => data.player.transform.position.IsWithinDistanceOf(transform.position, 10f);


    public override void OnEnterState() {
        UpdateStrategy();
    }

    public override void OnUpdateState() {
        _strategyDecisionCountdown -= Time.deltaTime;

        // Update strategy when countdown reached 0
        if (_strategyDecisionCountdown <= 0f) {
            UpdateStrategy();
            _strategyDecisionCountdown = Random.Range(_randomizedChangeStrategyTime.x, _randomizedChangeStrategyTime.y);
        }

        // Move towards player if not close enough to attack
        if (!isWithinNormalAttackRange) {
            Vector2 moveDirection = (data.player.transform.position - transform.position).normalized;
            moveDirection.y = 0f;

            _move = moveDirection * data.speed;
        }

        // Logic for the various attack strategies
        switch (data.currentAttackStrategy) {
            case PB_Data.AttackStrategy.NormalAttack: NormalAttackLogic(); break;
            case PB_Data.AttackStrategy.Charge: ChargeAttackLogic(); break;
            case PB_Data.AttackStrategy.Shockwave: ShockwaveAttackLogic(); break;
        }
    }

    public override void OnFixedUpdateState() {
        data.characterController2D.Move(_move != Vector2.zero, _move, false, false);
    }
    protected override void Initialize() {
        base.Initialize();

        _enemyCombat = GetComponentInParent<EnemyCombat>();
    }


    void NormalAttackLogic() {
        if (isWithinNormalAttackRange) {
            // Perform regular attack
            _enemyCombat.AttackIfPossible();
        }
    }

    void ChargeAttackLogic() {
        if (isWithinChargeAttackRange) {
            fsm.ChangeState<PB_ChargeState>();
        }
    }

    void ShockwaveAttackLogic() {
        if (isWithinShockwaveAttackRange) {
            fsm.ChangeState<PB_ShockwaveState>();
        }
    }

    // Will randomize attack strategy based on attack weights
    void UpdateStrategy() {
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
