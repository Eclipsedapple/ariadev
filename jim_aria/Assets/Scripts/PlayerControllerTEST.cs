using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTEST : MonoBehaviour 
{
	public float speed = 6f;
	public float jump_force = 1f;
	public float flap_force = 200f;
	public float flyDownAngle = 90f;
	public float flyUpAngle = -90f;
	public float antiDrift = 0.8f;
	private float antiGrav = 9f;

	public Transform main_camera;

	private bool stopped_movement = true;
	private bool is_grounded = true;
	private bool has_flapped = false;
	private bool isFlying = false;

	private Rigidbody rb;

	void Start()
	{
		main_camera = Camera.main.transform;
		rb = GetComponent<Rigidbody> ();
	}

	void Update()
	{
		if (!Input.GetButton ("Fire3")) 
		{
			// face the camera's direction
			transform.rotation = Quaternion.Euler (0.0f, main_camera.eulerAngles.y, 0.0f);
		}

		if (Input.GetButtonDown ("Jump") && !isFlying)
		{
			if (is_grounded)
			{
				rb.AddForce (Vector3.up * jump_force, ForceMode.VelocityChange);
				is_grounded = false;
			} else if (!has_flapped)
			{
				rb.AddForce (Vector3.up * flap_force, ForceMode.VelocityChange);
				has_flapped = true;
			} else
			{
				isFlying = true;
			}
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
					has_flapped = false;
					isFlying = false;
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

		if (isFlying)
		{
			Fly (h, v);
		}
		else
		{
			Walk (h, v);
		}
	}

	void Walk(float h, float v)
	{
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

	void Fly(float h, float v)
	{
		float u = Input.GetAxisRaw ("Jump");

		Vector3 movement = -transform.InverseTransformDirection (rb.velocity) * antiDrift;

		movement.z = v * 2 * speed;
		movement.y = u * speed + antiGrav;

		rb.AddRelativeForce (movement);
	}
}