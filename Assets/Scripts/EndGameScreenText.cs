using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameScreenText : MonoBehaviour {

	public Text endGameText;
	public static int finalDeathCount;
	// Use this for initialization
	void Start () {
		// Do you feel bad yet?
		endGameText.text="You have killed " + finalDeathCount + "people.";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
