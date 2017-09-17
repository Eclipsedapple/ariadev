using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public float speed = 6f;
	public float jump_force = 1f;

	public Transform main_camera;

	private bool stopped_movement = true;
	private bool is_grounded = true;

	private Rigidbody rb;

	void Start()
	{
		main_camera = Camera.main.transform;
		rb = GetComponent<Rigidbody> ();
	}

	void Update()
	{
		if (Input.GetButton ("Fire3")) 
		{
			// face the camera's direction
			transform.rotation = Quaternion.Euler (0.0f, main_camera.eulerAngles.y, 0.0f);
		}

		if (Input.GetButtonDown ("Jump") && is_grounded) 
		{
			rb.AddForce (Vector3.up * jump_force, ForceMode.VelocityChange);
			is_grounded = false;
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.CompareTag ("ground")) 
		{
			if(col.contacts.Length > 0)
			{
				ContactPoint contact = col.contacts[0];
				if(Vector3.Dot(contact.normal, Vector3.up) > 0.25)
				{
					is_grounded = true;
				}
			}
		}
	}

	void FixedUpdate()
	{
		// Get movement directions
		// Negative values represent left/backward movement
		// Positive values represent right/forward movement
		// Zero represents no movement
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		// If inputting movement
		if (h != 0f || v != 0f) 
		{
			// Multiply movement axis by speeds
			h *= Time.deltaTime * speed;
			v *= Time.deltaTime * speed;

			if (stopped_movement) 
			{
				transform.rotation = Quaternion.Euler (0.0f, main_camera.eulerAngles.y, 0.0f);
				stopped_movement = false;
			}

			// Move the player
			transform.position += Vector3.ClampMagnitude(transform.right * h + transform.forward * v, speed);
		} else 
		{
			stopped_movement = true;
		}
	}
}