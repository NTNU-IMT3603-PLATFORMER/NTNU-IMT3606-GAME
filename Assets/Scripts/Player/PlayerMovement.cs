using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField, Tooltip("Controller that will move character")]
    CharacterController2D _characterController;
    [SerializeField, Tooltip("Normal walking speed")]
    float _speed = 5f;
    [SerializeField, Tooltip("Speed when running")]
    float _runSpeed = 8f;

    /// <summary>
    /// Character controller used for player movement
    /// </summary>
    public CharacterController2D characterController => _characterController;

    /// <summary>
    /// runSpeed is the speed of the player when he/she is running (Holding SHIFT). 
    /// </summary>
    public float runSpeed => _runSpeed;

    /// <summary>
    /// speed is the speed of the player when he/she is not running.
    /// </summary>
    public float speed {
        get => _speed;
        set => _speed = value;
    }

    /// <summary>
    /// Is the player moving?
    /// </summary>
    public bool isMoving { get; private set; }

    /// <summary>
    /// Should the player run once it starts moving?
    /// Does not necessarily mean that the player is moving,
    /// only that the player should run if it begins to move
    /// </summary>
    public bool shouldRun { get; private set; }

    float _move;
    bool _jump;
    bool _dash;

    void Update() {
        // Only change running state when grounded
        // as it doesn't look right when you are able
        // to change speed in air
        if (characterController.isGrounded) {
            shouldRun = Input.GetButton("Run");
        }

        _move = Input.GetAxisRaw("Horizontal") * (shouldRun ? _runSpeed : _speed);

        if (Input.GetButton("Jump")) {
            if (characterController.currentJumps == 0) {
                _jump = true;
            } else if (Input.GetButtonDown("Jump")) {
                _jump = true;
            }
        }

        if (Input.GetButtonDown("Dash")) {
            _dash = true;
        }

        // If the player is running and is on the ground, then play the running sound
        if (isMoving && characterController.isGrounded) {
            AudioManager.instance.PlaySound("playerrun");
        } else {
            AudioManager.instance.StopSound("playerrun");
        }

        isMoving = _move != 0f;
    }

    void FixedUpdate() {
        _characterController.Move(Input.GetAxisRaw("Horizontal") != 0f, Vector2.right * _move, _jump, _dash);

        // Reset jump & dash so that input has to be captured again
        _jump = false;
        _dash = false;
    }

}
