﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayDeathCounter : MonoBehaviour {

	public Text deathCounterText;
	public int deathCount;

	// Use this for initialization
	void Start () {
		deathCounterText.text="" + deathCount;
		deathCount = 0;
	}

	// Update is called once per frame
	void Update () {
		deathCounterText.text="" + deathCount;
	}
	public void addScore(int i)
	{
		deathCount += i;
	}

	public int getScore()
	{
		return deathCount;
	}
}