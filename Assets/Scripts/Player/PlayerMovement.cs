using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[Tooltip("Controller that will move character")]
	public CharacterController2D characterController;
	[Tooltip("Normal walking speed")]
	public float speed = 5f;
	[Tooltip("Speed when running")]
	public float runSpeed = 8f;

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
		if (characterController.isGrounded) {
			shouldRun = Input.GetButton("Run");
		}

		float move = Input.GetAxisRaw("Horizontal") * (shouldRun ? runSpeed : speed);
		bool jump = Input.GetButton("Jump");

		isMoving = move != 0f;
        
		characterController.Move(Vector2.right * move, jump);
	}

}
