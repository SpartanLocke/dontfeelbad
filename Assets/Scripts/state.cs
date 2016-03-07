using UnityEngine;
using System.Collections;


public class state : MonoBehaviour {

	private string current_state;
	private bool changed;
	public GameObject happy;
	public GameObject upset;

//TODO, fix jenky, load all states to only load the state we need and the sprite we need, empty that single gameObject
//currently doesnt free up old states

	// Use this for initialization
	void Start () {
		current_state = "normal";
		if (current_state == "normal") {
			GameObject stateObj = new GameObject ();
			stateObj = (GameObject) Instantiate (happy, gameObject.transform.position, Quaternion.identity);
			stateObj.transform.parent = gameObject.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (changed) {
			GameObject stateObj = new GameObject ();
			if (current_state == "normal") {
				stateObj = (GameObject) Instantiate (happy, gameObject.transform.position, Quaternion.identity);
				stateObj.transform.parent = gameObject.transform;
			}
			if (current_state == "sad") {
				stateObj = (GameObject) Instantiate (upset, gameObject.transform.position, Quaternion.identity);
				stateObj.transform.parent = gameObject.transform;
			}

			changed = false;
		}
	}

	//valid params are currently 'normal', 'sad'
	public void changeState(string newState) {
		current_state = newState;
		changed = true;
	}
}
