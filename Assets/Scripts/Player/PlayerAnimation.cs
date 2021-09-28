using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    public Animator animator;
    public PlayerMovement playerMovement;

    void Update () {
        animator.SetBool("move", playerMovement.isMoving);
        animator.SetBool("grounded", playerMovement.characterController.isGrounded);
    }

}
