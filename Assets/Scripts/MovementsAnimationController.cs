using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class MovementsAnimationController : MonoBehaviour
{
    // Required components
    PlayerController controller;

    // Parameters
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        controller.AddJumpAction(OnJump);
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
		//Get controller velocity;
		Vector3 velocity = controller.GetVelocity();

		animator.SetFloat("velocity", velocity.magnitude);
		animator.SetBool("isGrounded", controller.IsGrounded());

        animator.SetBool("isJumping", false);
	}

    void OnJump()
    {
        Debug.Log("Jump anim!");
        animator.SetTrigger("jump");
    }
}
