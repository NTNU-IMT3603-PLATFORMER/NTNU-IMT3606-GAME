using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField, Tooltip("Controller that will move character")]    CharacterController2D _characterController;
	[SerializeField, Tooltip("Normal walking speed")]                   float _speed = 5f;
	[SerializeField, Tooltip("Speed when running")]                     float _runSpeed = 8f;

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

	void FixedUpdate () {
		// Only change running state when grounded
		// as it doesn't look right when you are able
		// to change speed in air
		if (_characterController.isGrounded) {
			shouldRun = Input.GetButton("Run");
		}

		float move = Input.GetAxisRaw("Horizontal") * (shouldRun ? _runSpeed : _speed);
		bool jump = Input.GetButton("Jump");

		isMoving = move != 0f;
        
		_characterController.Move(Input.GetAxisRaw("Horizontal") != 0f, Vector2.right * move, jump);
	}

}
