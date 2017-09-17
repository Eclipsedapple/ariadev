using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	public int startingHealth = 100;
	private int currentHealth;

	public RectTransform healthBar;
	public RectTransform enemyBar;

	private float last_hit_time;
	private float current_hit_time;

	private float barLength;
	private float healthPercent;

	/*private Color red = new Color(255, 0, 0, 0.5f);
	private Color clear = new Color(0, 0, 0, 0);
	private bool isRed;
	private float turned_red = 0;
	private float current_time;
	private float red_length = 0.3f;
	private Texture wood;*/

	public float invincibility_time;

	bool isDead;

	// Use this for initialization
	void Awake ()
	{
		currentHealth = startingHealth;
		last_hit_time = Time.time;
		/*isRed         = false;
		current_time  = Time.time;*/
	}

	void Update()
	{
		/*current_time = Time.time;

		if (isRed && current_time - turned_red > red_length)
		{
			GetComponent<Renderer> ().material.color = clear;
			isRed = false;
		}*/

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
		else if (gameObject.CompareTag ("Enemy"))
		{	
			healthPercent = ((float)currentHealth / (float) startingHealth);
			Debug.Log (healthPercent.ToString());
			barLength = (3.9f * healthPercent);
			enemyBar.sizeDelta = new Vector2 (barLength, enemyBar.sizeDelta.y);
			/*GetComponent<Renderer>().material.color = red;
			isRed = true;
			turned_red = Time.time;*/
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
}