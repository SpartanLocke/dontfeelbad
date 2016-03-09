using UnityEngine;
using System.Collections;

public class Retry : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp()
	{
		Debug.Log ("Reset Level");
		Application.LoadLevel (Application.loadedLevel);
	}
}
