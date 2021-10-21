using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    [SerializeField, Tooltip("Animator controlling animations")]        Animator _animator;

    PlayerMovement _playerMovement;
    PlayerCombat _playerCombat;
    PlayerTransformation _playerTransformation;

    void Start() {
        _playerMovement = GetComponentInParent<PlayerMovement>();
        _playerCombat = GetComponentInParent<PlayerCombat>();
        _playerTransformation = GetComponentInParent<PlayerTransformation>();

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
