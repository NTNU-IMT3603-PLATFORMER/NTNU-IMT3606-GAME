using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for controlling enemy animation
/// </summary>
public class EnemyAnimation : MonoBehaviour {

    const string ATTACK_TRIGGER = "attack";
    const string DEATH_TRIGGER = "death";

    [SerializeField, Tooltip("Animator controlling animations")]
    Animator _animator;

    EnemyCombat _enemyCombat;
    EnemyEntity _enemyEntity;

    void Start() {
        _enemyCombat = GetComponent<EnemyCombat>();
        _enemyEntity = GetComponent<EnemyEntity>();

        _enemyCombat.eventOnAttack.AddListener(AttackAnimationListener);
        _enemyEntity.eventOnDeath.AddListener(DeathAnimationListener);
    }

    void AttackAnimationListener(){
        _animator.SetTrigger("attack");
    }

    void DeathAnimationListener(){
        _animator.SetTrigger("death");
    }

}
