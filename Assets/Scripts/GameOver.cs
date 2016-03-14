using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {

	public GameObject traps;
	public Text GameEndTimer;
	public GameObject men;
	public GameObject cars;
	public GameObject endGameScreen;

	private bool endingGame;
	// Use this for initialization
	void Start () {
		endingGame = false;
	}

	// Update is called once per frame
	void Update () {
		// Check all children in folder to see if these objects still exist
		// If there are none, there should be 1 transform in the child
		if (!endingGame) {
			Transform[] trapArray = traps.GetComponentsInChildren<Transform> ();
			Transform[] menArray;
			Transform[] carArray;
			if (men == null)
				menArray = null;
			else
				menArray = traps.GetComponentsInChildren<Transform> ();
			if (cars == null)
				carArray = null;
			else
				carArray = cars.GetComponentsInChildren<Transform> ();
			// End the Game if there are no traps left, or if all men and cars died
			if (trapArray.Length <= 1) {
				StartCoroutine ("EndGame");
			} else if (menArray != null) {
				if (menArray.Length <= 1) {
					if (carArray != null) {
						if (carArray.Length <= 1) {
							StartCoroutine ("EndGame");
						}
					} else {
						StartCoroutine ("EndGame");
					}
				}
			}
		}
	}

	// Start Text Counter to end of game - load end game scene
	// 
	IEnumerator EndGame() {
		endingGame = true;
		for (int i = 10; i > 0; i--) {
			GameEndTimer.text="Game Ends in: " + i;
			yield return new WaitForSeconds (1);
		}
		// Load an End Game Screen or whatever here
		Application.LoadLevel ("EndScreen");

	}
}
