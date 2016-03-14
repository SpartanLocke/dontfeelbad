using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {

	public GameObject traps;
	public Text GameEndTimer;
	//public GameObject men;
	public GameObject cars;
	public GameObject endGameScreen;
	public static int finalDeathCount;
	public DisplayDeathCounter deathCounter;

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
			GameObject[] menArray;
			//Transform[] carArray;
			menArray = GameObject.FindGameObjectsWithTag("man");
			Debug.Log ("There are " + trapArray.Length + " traps left.");
			// End the Game if there are no traps left, or if all men  died
			if (trapArray.Length <= 1) {
				StartCoroutine ("EndGame");
			} else if (menArray != null) {
				Debug.Log ("There are " + menArray.Length + "people left");
				if (menArray.Length < 1) {
					StartCoroutine ("EndGame");
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
		finalDeathCount = deathCounter.getScore();
		Application.LoadLevel ("EndScreen");

	}
}
