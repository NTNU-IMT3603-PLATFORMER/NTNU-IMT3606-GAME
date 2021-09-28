using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D characterController;
	public float speed = 5f;
	public float runSpeed = 8f;

	public bool isMoving { get; private set; }
	public bool isRunning { get; private set; }

	void FixedUpdate () {
		// Only change running state when grounded
		// as it doesn't look right when you are able
		// to change speed in air
		if (characterController.isGrounded) {
			isRunning = Input.GetButton("Run");
		}

		float move = Input.GetAxisRaw("Horizontal") * (isRunning ? runSpeed : speed);
		bool jump = Input.GetButton("Jump");

		isMoving = move != 0f;
        
		characterController.Move(Vector2.right * move, jump);
	}

}
