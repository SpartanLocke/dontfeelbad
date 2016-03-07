using UnityEngine;
using System.Collections;

public class TrafficController : MonoBehaviour {


	public GameObject[] verticalLight;
	public GameObject[] horizontalLight;

	// Note: this is under the assumption that all vertical lights will act the same way and all horizontal will as well
	public bool verticalIsGreen()
	{
		return verticalLight[0].GetComponent<TrafficLight>().isGreen ();
	}

	public bool horizontalIsGreen()
	{
		return horizontalLight[0].GetComponent<TrafficLight>().isGreen ();
	}

	public void breakVertical()
	{
		foreach(GameObject light in verticalLight)
		{
			light.GetComponent<TrafficLight> ().breakLight ();
		}
	}

	public void breakHorizontal()
	{
		foreach(GameObject light in horizontalLight)
		{
			light.GetComponent<TrafficLight> ().breakLight ();
		}
	}

}
