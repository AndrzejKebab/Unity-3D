using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
	public CharacterController2D controller;
	public Animator animator;
	public float playerHealth;
	[SerializeField] private Text healthText;
	public GameObject DeathScreen;
	public GameObject UI;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;

	private void Start()
	{
		UpdateHealth();
	}

	// Update is called once per frame
	void Update()
	{
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("IsJumping", true);
		}
	
	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);

	}

	private void FixedUpdate()
	{
		controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
		jump = false;
	}

	public void UpdateHealth()
	{
		healthText.text = playerHealth.ToString("0");

		if (playerHealth <= 0)
		{
			DeathScreen.SetActive(true);
			UI.SetActive(false);
			Time.timeScale = 0f;
		}
	}
}
