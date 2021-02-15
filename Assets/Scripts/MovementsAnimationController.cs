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
    }

    // Update is called once per frame
    void Update ()
    {
		//Get controller velocity;
		Vector3 velocity = controller.GetVelocity();

		animator.SetFloat("velocity", velocity.magnitude);
		animator.SetBool("isGrounded", controller.IsGrounded());
	}
}
