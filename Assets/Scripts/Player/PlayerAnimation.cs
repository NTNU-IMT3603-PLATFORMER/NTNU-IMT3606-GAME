using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    [SerializeField, Tooltip("Animator controlling animations")]        Animator _animator;
    [SerializeField, Tooltip("PlayerMovement connected to player")]     PlayerMovement _playerMovement;
    [SerializeField, Tooltip("PlayerCombat connected to player")]     PlayerCombat _playerCombat;
    [SerializeField, Tooltip("PlayerTransformation connected to player")] PlayerTransformation _playerTransformation;

    private void Start() {
        _playerCombat.eventOnAttack.AddListener(AttackAnimationListener);
        _playerTransformation.eventOnTransform.AddListener(TransformAnimationListener);

    }
    void Update () {
        _animator.SetBool("move", _playerMovement.isMoving && !_playerMovement.characterController.isDashing);
        _animator.SetBool("grounded", _playerMovement.characterController.isGrounded);
        _animator.SetBool("facingWall", _playerMovement.characterController.isHuggingWall);
    }

    void AttackAnimationListener() {
        _animator.SetTrigger("isAttacking"); 
    }

    void TransformAnimationListener() {
        _animator.SetTrigger("isTransforming");
    }
}
