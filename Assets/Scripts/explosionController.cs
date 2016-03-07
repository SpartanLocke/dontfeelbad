using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class explosionController : MonoBehaviour {

	public int radius;
	private GameObject[] men;
	private GameObject[] text;
	//public DisplayDeathCounter deathCounter;
	// Use this for initialization
	void Start () {
		Debug.Log ("instantiated");
		men = GameObject.FindGameObjectsWithTag("man");
		StartCoroutine (explode ());
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (men.Length);
		foreach (GameObject man in men) {
			if (man != null) {
				float distanceSqr = (gameObject.transform.position - man.transform.position).sqrMagnitude;
				if (distanceSqr < radius) {
					text = GameObject.FindGameObjectsWithTag("deathCount");
					foreach (GameObject texts in text) {
						if (texts != null) {
							DisplayDeathCounter deathCounter = (DisplayDeathCounter)texts.GetComponent (typeof(DisplayDeathCounter));
							deathCounter.addScore (1);
						}
					}
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
