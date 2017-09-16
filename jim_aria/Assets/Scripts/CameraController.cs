using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	// Y rotation min and max angles
	private const float Y_ANGLE_MIN = 0.0f;
	private const float Y_ANGLE_MAX = 50.0f;

	public Transform target;
	public Transform cam_transform;

	public float x_sensitivity = 4.0f;
	public float y_sensitivity = 1.0f;
	public bool invert_x = false;
	public bool invert_y = false;

	private float distance = 10.0f;
	private float current_x = 0.0f;
	private float current_y = 0.0f;

	void Start()
	{
		cam_transform = transform;
	}

	void Update()
	{
		// Get mouse x input
		if(!invert_x)
			current_x -= Input.GetAxis ("Mouse X") * x_sensitivity;
		else if(invert_x)
			current_x += Input.GetAxis ("Mouse X") * x_sensitivity;

		// Get mouse y input
		if(!invert_y)
			current_y += Input.GetAxis ("Mouse Y") * y_sensitivity;
		else if(invert_y)
			current_y -= Input.GetAxis ("Mouse Y") * y_sensitivity;

		// Clamp y rotation
		current_y = Mathf.Clamp(current_y, Y_ANGLE_MIN, Y_ANGLE_MAX);
	}

	void LateUpdate()
	{
		// Distance from target
		Vector3 dir = new Vector3 (0.0f, 0.0f, - distance);

		// Rotation based on mouse input 
		Quaternion rotation = Quaternion.Euler(current_y, current_x, 0.0f);

		// Move camera position
		cam_transform.position = target.position + rotation * dir;

		// Look at target object
		cam_transform.LookAt (target.position);
	}
}