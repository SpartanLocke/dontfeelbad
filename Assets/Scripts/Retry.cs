﻿using UnityEngine;
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
        Application.LoadLevel("main");
    }


	public void restart()
	{
		//Application.LoadLevel (Application.loadedLevel);
		Application.LoadLevel ("main");
	}
}
