using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D cc;
	public float speed = 5f;

	public bool running { get; private set; }

	private float move;
	private bool jump;

	void Update () {
		move = Input.GetAxisRaw("Horizontal") * speed;
		jump = Input.GetButton("Jump");
		running = move != 0f;
        
		cc.Move(Vector2.right * move, jump);
	}

}
