using UnityEngine;
using System.Collections;

public class explosionController : MonoBehaviour {

	public int radius;
	private GameObject[] men;
	// Use this for initialization
	void Start () {
		Debug.Log ("instantiated");
		men = GameObject.FindGameObjectsWithTag("man");
		StartCoroutine (explode ());
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (men.Length);
		foreach (GameObject man in men) {
			if (man != null) {
				float distanceSqr = (gameObject.transform.position - man.transform.position).sqrMagnitude;
				if (distanceSqr < radius) {
					Destroy (man);
				}
			}
		}
	}

	IEnumerator explode()
	{
		yield return new WaitForSeconds (1);
		Destroy (gameObject);
	}
}
