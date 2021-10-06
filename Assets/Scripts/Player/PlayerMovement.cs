using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField, Tooltip("Controller that will move character")]                                    CharacterController2D _characterController;
	[SerializeField, Tooltip("Normal walking speed")]                                                   float _speed = 5f;
	[SerializeField, Tooltip("Speed when running")]                                                     float _runSpeed = 8f;
	[SerializeField, Tooltip("Will trigger dash if pressing dash button twice within this interval")]   float _dashInputInterval = 0.5f;

	/// <summary>
	/// Character controller used for player movement
	/// </summary>
	public CharacterController2D characterController => _characterController;

	/// <summary>
	/// Is the player moving?
	/// </summary>
	public bool isMoving { get; private set; }

	/// <summary>
	/// Should the player run once it starts moving?
	/// Does not necessarily mean that the player is moving,
	/// only that the player should run if it begins to move
	/// </summary>
	public bool shouldRun { get; private set; }

	float _move;
	bool _jump;
	bool _dash;

	void Update () {
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

		isMoving = _move != 0f;
	}

	void FixedUpdate () {
		_characterController.Move(Input.GetAxisRaw("Horizontal") != 0f, Vector2.right * _move, _jump, _dash);
		
		// Reset jump & dash so that input has to be captured again
		_jump = false;
		_dash = false;
	}

}
