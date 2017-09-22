using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : MonoBehaviour {

	int damage = 20;
	float lifetime = 0.25f;
	float rise_speed = 6f;
	float expand_speed = 20f;
	float push_speed = 200f;

	// Knockback strength
	//public float gust_knockback = 7f;

	void Awake()
	{
		Destroy (gameObject, lifetime);
	}

	void OnTriggerEnter(Collider col)
	{
		GameObject hit = col.gameObject;
		Health health = hit.GetComponent<Health> ();

		UnityEngine.UI.Text debug = GameObject.Find ("Debug").GetComponent<UnityEngine.UI.Text> ();
		if (debug != null)
		{
			//debug.text = "Collided with gameobject: " + hit.name;
		}

		if (hit.CompareTag ("Enemy") && health != null) 
		{
			health.TakeDamage (damage);

			Vector3 pushDir = (hit.transform.position - transform.position);
			pushDir.Normalize ();
			pushDir += transform.up;

			Rigidbody moveable = hit.GetComponent<Rigidbody> ();
			if (moveable != null)
			{
				moveable.AddForce (pushDir * push_speed);
				if (debug != null)
				{
					debug.text = "Force: " + pushDir;
				}
			}
		}
	}

	void Update()
	{
		transform.position += transform.up * rise_speed * Time.deltaTime;
		transform.localScale += (transform.right + transform.forward) * expand_speed * Time.deltaTime;
	}
}
