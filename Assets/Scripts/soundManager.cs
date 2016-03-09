using UnityEngine;
using System.Collections;

public class soundManager : MonoBehaviour {

    public AudioSource[] sounds;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void sparta()
    {
        sounds[1].Play();
    }
    public void wilhelm()
    {
        sounds[0].Play();
    }
}
