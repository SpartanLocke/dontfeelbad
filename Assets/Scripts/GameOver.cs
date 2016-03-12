using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {

	public GameObject traps;
	public Text GameEndTimer;
	//public GameObject men;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		Transform[] trapArray = traps.GetComponentsInChildren<Transform> ();
		//Transform[] menArray = traps.GetComponentsInChildren<Transform> ();

		if (trapArray.Length <=1 /*|| menArray.Length <=1*/) {
			StartCoroutine ("EndGame");
		}
	
	}

	// Start Text Counter to end of game - load end game scene
	// 
	IEnumerator EndGame() {
		for (int i = 10; i > 0; i--) {
			GameEndTimer.text="Game Ends in: " + i;
			yield return new WaitForSeconds (1);
		}
		// Load an End Game Screen or whatever here
		Application.LoadLevel ("main");
	}
}
