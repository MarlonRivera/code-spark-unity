using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public CharacterController2D controller;
	public Animator animator;

	public float runSpeed = 50;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	PlayerState currentState;


	void FixedUpdate()
	{
		// Get player state
		currentState = PlayerStateManager.Instance.GetCurrentState();
		if (currentState == PlayerState.ALIVE)
		{
			// Move our character
			controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		}
		jump = false;
	}

	// Update is called once per frame
	void Update()
	{

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		animator.SetFloat("speed", Mathf.Abs(horizontalMove));


		if (Input.GetButtonDown("Jump") && currentState == PlayerState.ALIVE)
		{
			jump = true;
			animator.SetBool("isJumping", true);
		}
	}

	public void OnLanding()
	{
		animator.SetBool("isJumping", false);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "spikes")
		{
			animator.SetTrigger("Death");
			PlayerStateManager.Instance.SetCurrentSate(PlayerState.DEAD);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log(collision.gameObject.name);
	}
}