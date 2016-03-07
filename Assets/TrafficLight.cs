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

	public void breakLight()
	{
		broken = true;
		StopCoroutine (setLight ());
		// if light is broken - permanently green
		state = 0;
		GetComponent<Renderer> ().material.color = new Color (0, 1, 0);
	}
	IEnumerator setLight()
	{
		
    while(!broken)
    {
			if (northSouth) {
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
