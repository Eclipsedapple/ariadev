using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private int enemiesLeft = 0;

	private GameObject[] enemies_in_game;
	//private GameObject win_object;

	public GameObject text_object;

	private Color gold = new Color(201, 168, 30, 255);

	// Use this for initialization
	void Start ()
	{
		enemies_in_game = GameObject.FindGameObjectsWithTag ("Enemy");
		enemiesLeft = enemies_in_game.Length;
	}
	
	// Update is called once per frame
	void Update ()
	{
		enemies_in_game = GameObject.FindGameObjectsWithTag ("Enemy");
		enemiesLeft = enemies_in_game.Length;

		if (enemiesLeft <= 0)
		{
			winGame ();
		}
	}

	void winGame()
	{
		text_object.GetComponent<Text>().text = "You done it!";
	}

	/*void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.CompareTag ("Win") && enemiesLeft <= 0) 
		{
           //
		}
	}*/
}