using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gust : MonoBehaviour 
{
	public const int damage = 10;

	void OnCollisionEnter(Collision col)
	{
		GameObject hit = col.gameObject;

		if (CompareTag ("Enemy")) 
		{
			Debug.Log ("HIT!");
		}

	}
}
