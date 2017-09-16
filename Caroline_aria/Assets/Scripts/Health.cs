using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	public int startingHealth = 100;
	public static int currentHealth;
	public int scoreValue = 1;

	public RectTransform healthBar;

	bool isDead;

	// Use this for initialization
	void Awake ()
	{
		currentHealth = startingHealth;
	}

	public void TakeDamage (int amount)
	{
		if (isDead)
			return;

		currentHealth -= amount;
		healthBar.sizeDelta = new Vector2 (currentHealth, healthBar.sizeDelta.y);

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