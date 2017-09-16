using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour 
{

	// Movement speed
	public float speed = 6f;

	// Grounded status
	public bool isGrounded;

	// Mouse rotation speed
	public float turn_speed = 150f;

	// Offset of the camera
	public Vector3 cam_dist = new Vector3(0f, 3.5f, 7f);

	// Is the camera behind the player
	private bool is_behind = false;

	private Transform main_camera;

	void Start()
	{

		// Find the camera's transform
		main_camera = transform.Find("Main Camera");

		// Move the camera behind the player
		ResetCamera ();
	}

	void FixedUpdate()
	{
		// Get movement direction axis
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		// Get mouse direction axis
		float m_x = Input.GetAxis ("Mouse X");
		float m_y = Input.GetAxis ("Mouse Y");

		// Multiply movement axis by speeds
		h *= Time.deltaTime * speed;
		v *= Time.deltaTime * speed;

		// Multiply mouse axis by turn speed
		m_x *= Time.deltaTime * turn_speed;
		m_y *= Time.deltaTime * turn_speed;

		// Move the player
		transform.position += (transform.right * h + transform.forward * v);

		// Rotate camera around player on x-axis
		//main_camera.RotateAround (transform.position, Vector3.right, m_y * turn_speed * Time.deltaTime);

		if (h != 0f || v != 0f) 
		{
			// If the player is moving

			if (!is_behind) 
			{
				// Move the camera behind the player
				ResetCamera ();
				is_behind = true;
			}

			// Rotate the player based on mouse movement
			transform.Rotate (0f, m_x, 0f);
		}
		else if (h == 0f && v == 0f) 
		{
			// If the player is not moving 

			// Rotate the camera around the player on y-axis
			main_camera.RotateAround (transform.position, Vector3.up, m_x * turn_speed * Time.deltaTime);
			is_behind = false;
		}
			
	}

	void ResetCamera()
	{
		main_camera.position = (-cam_dist.z * transform.forward + transform.position);
		main_camera.position += new Vector3 (0f, cam_dist.y, 0f);

		main_camera.LookAt (transform);
	}
}