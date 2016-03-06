using UnityEngine;
using System.Collections;

public class TrafficLight : MonoBehaviour {

    
	public bool northSouth;
	private bool broken;
	private int state = 0;
   	void Start()
    {
		broken = false;
     StartCoroutine(setLight());
     
    }

	
	// Update is called once per frame
	void Update () {
	
	}

	public bool isGreen()
	{
		return (state == 0);
	}

	IEnumerator setLight()
	{
		
    while(true && !broken)
    {
			if (northSouth) {
				Debug.Log ("In here");
				state = 0;
				GetComponent<Renderer> ().material.color = new Color (0, 1, 0);
				yield return new WaitForSeconds (3);
				state = 1;
				GetComponent<Renderer> ().material.color = new Color (1, 1, 0);
				yield return new WaitForSeconds (3);
				state = 2;
				GetComponent<Renderer> ().material.color = new Color (1, 0, 0);
				yield return new WaitForSeconds (6);
			} else {
				Debug.Log ("In here");
				state = 2;
				GetComponent<Renderer> ().material.color = new Color (1, 0, 0);
				yield return new WaitForSeconds (6);
				state = 0;
				GetComponent<Renderer> ().material.color = new Color (0, 1, 0);
				yield return new WaitForSeconds (3);
				state = 1;
				GetComponent<Renderer> ().material.color = new Color (1, 1, 0);
				yield return new WaitForSeconds (3);

			}
    }

	}
}
