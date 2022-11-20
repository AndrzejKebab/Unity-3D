using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	private float inputX;
	private float inputZ;
	private float lookX;
	private float lookY;
	private float xRotation = 0f;
	public float gravity = -9.81f;

	private Camera _camera;
	private Rigidbody playerRb;
	private Transform player;

	public int health = 100;
	public float movespeed = 5f;
	


	// Start is called before the first frame update
	void Start()
	{
		player = GetComponent<Transform>();
		playerRb = GetComponent<Rigidbody>();
		_camera = Camera.main;
		Cursor.lockState = CursorLockMode.Locked;
	}

	void FixedUpdate()
	{
		playerRb.velocity = new Vector3(inputX * movespeed, 0, inputZ * movespeed);

		xRotation -= lookY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		_camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

		player.transform.Rotate(Vector3.up * lookX);
		
	}

	public void Move(InputAction.CallbackContext ctx)
	{
		inputX = ctx.ReadValue<Vector2>().x;
		inputZ = ctx.ReadValue<Vector2>().y;
	}

	public void Look(InputAction.CallbackContext ctx)
	{
		lookX = ctx.ReadValue<Vector2>().x;
		lookY = ctx.ReadValue<Vector2>().y;
	}
}
