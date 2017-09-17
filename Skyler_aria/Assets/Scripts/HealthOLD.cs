using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOLD : MonoBehaviour
{
	public int startingHealth = 100;
	public static int currentHealth;
	public int scoreValue = 1;

	public RectTransform healthBar;

	private float last_hit_time;
	private float current_hit_time;

	public float invincibility_time;

	bool isDead;

	// Use this for initialization
	void Awake ()
	{
		currentHealth = startingHealth;
		last_hit_time = Time.time;
	}

	public void TakeDamage (int amount)
	{
		if (isDead)
			return;

		current_hit_time = Time.time;

		// Check to see if it has been at least 2 seconds since the last hit
		if (current_hit_time - last_hit_time < invincibility_time)
		{
			return;
		}

		last_hit_time = current_hit_time;
		currentHealth -= amount;
		if (gameObject.CompareTag ("Player"))
		{
			healthBar.sizeDelta = new Vector2 (currentHealth, healthBar.sizeDelta.y);
		}
		last_hit_time = Time.time;

		if (currentHealth <= 0)
			Death ();
	}

	void Death()
	{
		isDead = true;
		if (gameObject.CompareTag ("Player"))
		{
			transform.position += (transform.up * -2);
		}
		else if (gameObject.CompareTag ("Enemy"))
		{
			Destroy (gameObject);
		}
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Comma) && gameObject.CompareTag ("Player"))
		{
			TakeDamage (25);
			Debug.Log ("Current player health: " + currentHealth.ToString ());
		}
		else if (Input.GetKeyDown (KeyCode.Period) && gameObject.CompareTag ("Enemy"))
		{
			TakeDamage (25);
			Debug.Log ("Current enemy health: " + currentHealth.ToString ());
		}
	}
}