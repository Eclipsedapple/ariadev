using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public int startingHealth = 100;
	public int currentHealth;
	public int scoreValue = 1;

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

		if (currentHealth <= 0)
			Death ();
	}

	void Death()
	{
		isDead = true;
		Destroy (gameObject);
	}
}
