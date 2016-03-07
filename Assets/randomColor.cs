using UnityEngine;
using System.Collections;

public class randomColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int n = Random.Range (0, 6);
		Color[] colors = new Color[] { Color.black, Color.blue, Color.green, Color.red, Color.yellow, Color.white};
		gameObject.GetComponent<SpriteRenderer> ().color = colors[n];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
