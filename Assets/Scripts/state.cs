﻿using UnityEngine;
using System.Collections;


public class state : MonoBehaviour {

	private string current_state;
	//private bool changed;
	public GameObject happy_state;
	public GameObject upset_state;
	public GameObject berserk_state;
	public GameObject scared_state;
	GameObject stateObj = new GameObject ();

//TODO, fix jenky, load all states to only load the state we need and the sprite we need, empty that single gameObject
//currently doesnt free up old states

	// Use this for initialization
	void Start () {
		current_state = "normal";
		if (current_state == "normal") {
			//GameObject stateObj = new GameObject ();
			stateObj = (GameObject) Instantiate (happy_state, gameObject.transform.position, Quaternion.identity);
			stateObj.transform.parent = gameObject.transform;
			stateObj.transform.localScale = new Vector3(2,2,1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//
		bool scared = gameObject.GetComponentInParent<movement>().scared;
		bool berserk = gameObject.GetComponentInParent<movement>().berserk;
		Debug.Log ("scared: " + scared);
		Debug.Log ("berserk: " + berserk);

		if (scared && current_state != "scared") {
			//GameObject stateObj = new GameObject ();
			stateObj = (GameObject)Instantiate (scared_state, gameObject.transform.position, Quaternion.identity);
			stateObj.transform.parent = gameObject.transform;
			stateObj.transform.localScale = new Vector3(2,2,1);
			current_state = "scared";
		} else if (berserk && current_state != "berserk") {
			//GameObject stateObj = new GameObject ();
			stateObj = (GameObject)Instantiate (berserk_state, gameObject.transform.position, Quaternion.identity);
			stateObj.transform.parent = gameObject.transform;
			stateObj.transform.localScale = new Vector3(2,2,1);
			current_state = "berserk";
		} else if (!scared && !berserk && current_state != "normal") {
			//GameObject stateObj = new GameObject ();
			stateObj = (GameObject)Instantiate (happy_state, gameObject.transform.position, Quaternion.identity);
			stateObj.transform.parent = gameObject.transform;
			stateObj.transform.localScale = new Vector3(2,2,1);
			current_state = "normal";
		}

		/*
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
		}*/
	}

	/*
	//valid params are currently 'normal', 'sad'
	public void changeState(string newState) {
		current_state = newState;
		changed = true;
	}*/
}
