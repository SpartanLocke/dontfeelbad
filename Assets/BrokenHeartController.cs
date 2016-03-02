using UnityEngine;
using System.Collections;

public class BrokenHeartController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(explode());
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator explode()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
