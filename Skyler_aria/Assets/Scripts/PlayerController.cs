using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public float speed = 6f;
	public float jump_force = 1f;
	public float fire_rate = 3f;
	public float gust_knockback = 7f;

	public Transform main_camera;
	public GameObject gust_prefab;
	public Transform gust_spawn;

	private bool is_grounded = true;

	private float prev_attack_time;
	private float curr_attack_time;

	private Rigidbody rb;
	private Animation ground_flap;

	void Start()
	{
		main_camera = Camera.main.transform;
		rb = GetComponent<Rigidbody> ();
		ground_flap = transform.Find ("Test Wings").gameObject.GetComponent<Animation> ();
		prev_attack_time = 0f;
		curr_attack_time = Time.time;
	}

	void Update()
	{
		if (Input.GetButtonDown("Fire2"))
		{
			curr_attack_time = Time.time;
			if (curr_attack_time - prev_attack_time > fire_rate)
			{
				Instantiate (gust_prefab, gust_spawn.position, gust_spawn.rotation);
				ground_flap.Play ();
				rb.AddForce (-transform.forward * gust_knockback, ForceMode.VelocityChange);
				prev_attack_time = curr_attack_time;
			}
		}

		if (!Input.GetButton ("Fire3")) 
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

			// Move the player
			transform.position += Vector3.ClampMagnitude(transform.right * h + transform.forward * v, speed);
		}
	}
}
