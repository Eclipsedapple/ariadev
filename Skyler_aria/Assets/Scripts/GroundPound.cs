using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : MonoBehaviour {

	public int damage = 20;
	public float lifetime = 0.25f;
	public float rise_speed = 6f;
	public float expand_speed = 20f;

	// Knockback strength
	//public float gust_knockback = 7f;

	void Awake()
	{
		Destroy (gameObject, lifetime);
	}

	void OnCollisionEnter(Collision col)
	{
		GameObject hit = col.gameObject;
		Health health = hit.GetComponent<Health> ();

		if (hit.CompareTag ("Enemy") && health != null) 
		{
			health.TakeDamage (damage);

			// pushing back the enemy
			//hit.GetComponent<Rigidbody> ().AddForce((hit.transform.position - col.contacts[0].point) * gust_knockback, ForceMode.VelocityChange);
		}
	}

	void Update()
	{
		transform.position += transform.up * rise_speed * Time.deltaTime;
		transform.localScale += (transform.right + transform.forward) * expand_speed * Time.deltaTime;
	}
}
