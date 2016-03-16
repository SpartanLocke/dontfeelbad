using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class explosionController : MonoBehaviour {

	private GameObject[] text;
	public float duration;
    public float delay;
    public Vector3 maxScale;
    public Vector3 minScale;

    public float radius;
    public float numFrames1;
    public float numFrames2;
    private float size = 0;
    private int counter = 0;
    //TODO: Take in radius and then modify the circle collider radius on spawn
    //public DisplayDeathCounter deathCounter;
    // Use this for initialization
    void Start () {
		CircleCollider2D explosionCollider = gameObject.GetComponent<CircleCollider2D>();
        explosionCollider.enabled = false;
        //gameObject.GetComponent<SpriteRenderer>().enabled = false;

        explosionCollider.radius = radius;
		
	}
	
	// Update is called once per frame
	void Update () {
        if (size < 1.0f)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, .3f);
            size = size + 1 / numFrames1;
            //Debug.Log(size);
            gameObject.transform.localScale = Vector3.Lerp(maxScale, minScale, size);
        }
        else
        {
            if (counter < numFrames2)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, .7f);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(114,46,61,.2f);
                gameObject.GetComponent<CircleCollider2D>().enabled = true;
                counter++;
            }
            else
            {
                GameObject.FindGameObjectWithTag("soundManager").GetComponent<soundManager>().explode();
                Destroy(gameObject);
            }
        }
    }

	void OnCollisionEnter2D(Collision2D coll){
		Debug.Log ("Explosion");
		if (coll.gameObject.tag == "car") {
			
			Destroy (coll.gameObject);
		}
        else if (coll.gameObject.tag == "man")
        {
            if (coll.collider == coll.gameObject.GetComponent<BoxCollider2D>())
            {
                Destroy(coll.gameObject);
            }
        }
    }
	
}