using UnityEngine;

/// <summary>
/// PlayerAnimation listens to unity events and play their respective animations.
/// </summary>
public class PlayerAnimation : MonoBehaviour {

    [SerializeField, Tooltip("Animator controlling animations")]
    Animator _animator;

    PlayerMovement _playerMovement;
    PlayerCombat _playerCombat;

    void Start() {
        _playerMovement = GetComponentInParent<PlayerMovement>();
        _playerCombat = GetComponentInParent<PlayerCombat>();

        _playerCombat.eventOnAttack.AddListener(AttackAnimationListener);
    }

    void Update() {
        _animator.SetBool("move", _playerMovement.isMoving && !_playerMovement.characterController.isDashing);
        _animator.SetBool("grounded", _playerMovement.characterController.isGrounded);
        _animator.SetBool("facingWall", _playerMovement.characterController.isHuggingWall);
    }

    void AttackAnimationListener() {
        _animator.SetTrigger("isAttacking");
    }

}
