using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
	[SerializeField] private Camera _camera;
	[SerializeField] private Transform player;

	private Vector3 cameraOffset;
	private float mouseScrollInput;
	private float camFOV;
	[SerializeField] private float zoomSpeed;

	[Range(0.01f, 1.0f)]
	[SerializeField] private float smoothness = 0.5f;

	// Start is called before the first frame update
	void Start()
	{
		cameraOffset = transform.position - player.transform.position;
		camFOV = _camera.fieldOfView;
	}

	// Update is called once per frame
	void Update()
	{
		mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");
		camFOV -= mouseScrollInput * zoomSpeed;
		camFOV = Mathf.Clamp(camFOV, 30, 90);

		_camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, camFOV, zoomSpeed);

		Vector3 newPos = player.position + cameraOffset;
		transform.position = Vector3.Slerp(transform.position, newPos, smoothness);
	}
}
