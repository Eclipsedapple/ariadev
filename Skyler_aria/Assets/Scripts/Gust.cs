using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gust : MonoBehaviour 
{
	public int damage = 10;
	public float lifetime = 0.5f;
	public float speed = 6f;
	public float gust_knockback = 7f;

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
			hit.GetComponent<Rigidbody> ().AddForce((hit.transform.position - col.contacts[0].point) * gust_knockback, ForceMode.VelocityChange);
		}
		else if(hit.CompareTag("Wall"))
		{
			Destroy (gameObject);
		}

	}

	void Update()
	{
		transform.position += transform.forward * speed * Time.deltaTime;
	}
}
