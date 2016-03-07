using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {

	public GameObject explosion;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Stepped on Mine");
		Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
		Destroy (gameObject);

	}
}
