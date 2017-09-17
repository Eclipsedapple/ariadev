using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController: MonoBehaviour 
{
	public float speed = 6f;
	public float jump_force = 1f;
	public float fire_rate = 3f;
	public float gust_knockback = 7f;

	public float flap_force = 200f;
	public float flyDownAngle = 90f;
	public float flyUpAngle = -90f;
	//public float antiDrift = 0.8f;

	public Transform main_camera;
	public GameObject gust_prefab;
	public Transform gust_spawn;

	private bool is_grounded = true;
	private bool has_flapped = false;
	private bool isFlying = false;

	private float prev_attack_time;
	private float curr_attack_time;

	private Rigidbody rb;
	private Animation ground_flap;

	// Used for debugging
	//public Text text;

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
				main_camera.GetComponent<CameraController> ().FlyMode = true;
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
					main_camera.GetComponent<CameraController> ().FlyMode = false;
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

			// Move the player
			transform.position += Vector3.ClampMagnitude(transform.right * h + transform.forward * v, speed);
		} 
	}

	void Fly(float h, float v)
	{
		float u = Input.GetAxisRaw ("Jump");

		//Vector3 movement = transform.InverseTransformDirection (main_camera.eulerAngles);
		//bool rising = movement.y < 0;

		float x = h * speed;
		float y = v * -Mathf.Sin (dtr(main_camera.eulerAngles.x)) * speed;
		float z = v * Mathf.Cos (dtr(main_camera.eulerAngles.x)) * speed;

		/*text.text = "cam x: " + main_camera.eulerAngles.x
		+ "\ncam y: " + main_camera.eulerAngles.y
		+ "\ncam z: " + main_camera.eulerAngles.z
			+ "\ncam x sin: " + Mathf.Sin (dtr(main_camera.eulerAngles.x));*/

		rb.velocity = transform.TransformDirection(new Vector3(x, y, z));
	}

	float dtr(float degrees)
	{
		return degrees * Mathf.PI / 180;
	}
}