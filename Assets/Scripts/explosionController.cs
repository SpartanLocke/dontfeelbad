using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class explosionController : MonoBehaviour {

	private GameObject[] men;
	private GameObject[] text;
	private GameObject[] cars;
	public float duration;
	//public DisplayDeathCounter deathCounter;
	// Use this for initialization
	void Start () {
		men = GameObject.FindGameObjectsWithTag("man");
		cars = GameObject.FindGameObjectsWithTag ("car");
		StartCoroutine (explode ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other){
		Debug.Log ("Explosion");
		if (other.gameObject.tag == "man" || other.gameObject.tag == "car") {
			text = GameObject.FindGameObjectsWithTag("deathCount");
			foreach (GameObject texts in text) {
				if (texts != null) {
					DisplayDeathCounter deathCounter = (DisplayDeathCounter)texts.GetComponent (typeof(DisplayDeathCounter));
					deathCounter.addScore (1);
				}
			}
			Destroy (other.gameObject);
		}
	}
	IEnumerator explode()
	{
        yield return new WaitForSeconds (duration);
		Destroy (gameObject);
	}
}
