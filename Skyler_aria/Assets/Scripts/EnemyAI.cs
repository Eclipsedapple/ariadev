using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	public Transform playerObject;

	private Vector3 playerPos;
	private Vector3 enemyPos;

	private bool isGrounded;

	public int attackDistance;

	// Movement speed
	public float speed = 6f;

	void Awake()
	{
		playerPos = playerObject.transform.position;
		enemyPos  = transform.position;
		isGrounded = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		playerPos = playerObject.transform.position;
		enemyPos  = transform.position;

		if (nearPlayer())
		{
			transform.position += (transform.up * 0.1f);
			if (isGrounded == true)
			{
				isGrounded = false;
			}
		}
		else
		{
			/*Debug.Log ("Recognizes player is no longer near");*/
			if (isGrounded == false)
			{
				transform.position -= (transform.up * 0.1f);
			}
		}
	}

	bool nearPlayer()
	{
		if (Vector3.Distance (playerPos, enemyPos) < attackDistance)
		{
			Debug.Log ("Enemy is near player!");
			return true;
		}
		return false;
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.CompareTag ("ground"))
		{
			Debug.Log ("Collission detected!");
			isGrounded = true;
		}
	}
}