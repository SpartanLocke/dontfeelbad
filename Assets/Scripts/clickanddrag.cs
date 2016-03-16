using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//[RequireComponent(typeof(SphereCollider))]

public class clickanddrag : MonoBehaviour
{
    public float speed = 1.0f;
    public float scale;
	public GameObject landMine;
    public GameObject meteorExplosion;
    public GameObject meteorVisualization;
    public HeartAttack ha;
	public TrafficController trafficController;
    public turnBerserk tb;
    private Vector3 originalSize;
    private Vector3 originalPos;
    public Vector3 point;
    private bool moved = false;
	private bool untouched = true;
	private bool dragging = false;
	public Text tutorialText;
	public Text tutorialText2;

	private Color oldColor;


    void Start()
    {
        originalPos = gameObject.transform.position;
		oldColor = gameObject.GetComponent<SpriteRenderer> ().color;
		tutorialText = GameObject.Find("tutorialText").GetComponent<Text>();
		tutorialText2 = GameObject.Find("tutorialText2").GetComponent<Text>();
    }

    void OnMouseDown()
    {
        if (!moved)
        {
            UnityEngine.Cursor.visible = false;
            originalSize = gameObject.transform.localScale;
            gameObject.transform.localScale = originalSize * scale;
        }
        if (gameObject.GetComponent<AudioSource>() != null)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }

    }
    void OnMouseDrag()
    {
        if (!moved)
        {
            point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point.z = gameObject.transform.position.z;
            gameObject.transform.position = point;
            
        }
		Debug.Log ("DRAGGING MODE");
		untouched = false;
		tutorialText.text = "Now drag the trap onto something it can be used on!";
		tutorialText2.text = "Now drag the trap onto something it can be used on!";

		if (gameObject.tag == "meteor" || gameObject.tag == "trafficTrap") {
			//for now, make it green whereever
			//TODO, limit by where i can place the meteor
			//gameObject.GetComponent<SpriteRenderer> ().color = new Color (0, 128, 0, .75f);
			gameObject.transform.localScale = originalSize * scale * 2;
		} else {
			/*
			//otherwise, gray for everything else
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (128, 128, 0, .75f);
			Debug.Log ("GRAY HOVER");
			*/
		}
		//gameObject.transform.localScale = originalSize * scale;
    }

	void OnCollisionEnter2D(Collision2D other) {
		//Debug.Log (other.gameObject.name);
		if (moved) {
			Debug.Log (1);
			if (other.gameObject.tag != "explosion") {
				Debug.Log (2);
				dragging = true;
				//Debug.Log ("tag is: " + other.gameObject.tag);
				if (gameObject.tag != "meteor" && gameObject.tag != "trafficTrap") {
					Debug.Log (3);
					if (other.gameObject.tag == "man") {
						//gameObject.GetComponent<SpriteRenderer> ().color = new Color(0,20,0,.75f);
						gameObject.transform.localScale = originalSize * scale * 2;
						Debug.Log ("size: " + (originalSize * scale * 2).ToString());
						//Debug.Log ("GREEN");
					} else {
						//gameObject.GetComponent<SpriteRenderer> ().color = new Color (128, 128, 128, .75f);
						gameObject.transform.localScale = originalSize * scale;
						//Debug.Log ("GRAY");
						//Debug.Log("GRAY COLLIDE");
					}
				}
			}
		}
		if (!untouched && (gameObject.tag == "heartattack" || gameObject.tag == "shooter")) {
			dragging = true;
			if (other.gameObject.tag == "man") {
				gameObject.transform.localScale = originalSize * scale * 2;
			} else {
				gameObject.transform.localScale = originalSize * scale;
			}
		}
	}

	void OnCollisionExit2D(Collision2D other) {
		if (moved) {
			if (other.gameObject.tag != "explosion") {
				gameObject.GetComponent<SpriteRenderer> ().color = oldColor;
				if (dragging) {
					gameObject.transform.localScale = originalSize * scale;
				} else {
					gameObject.transform.localScale = originalSize;
				}
			}
		}
		if (!untouched && (gameObject.tag == "heartattack" || gameObject.tag == "shooter")) {
			Debug.Log ("FFF: " + dragging);
			if (dragging) {
				Debug.Log ("GG");
				gameObject.transform.localScale = originalSize * scale;
			} else {
				gameObject.transform.localScale = originalSize;
			}
		}
	}

    void OnMouseUp()
    {
        moved = true;
        UnityEngine.Cursor.visible = true;

        if (gameObject.tag == "heartattack") //TODO: move this logic outside
        {
			tutorialText.text = "";
			tutorialText2.text = "";

            bool killed = ha.activate(point);
            if (killed)
            {
                
                Destroy(gameObject);


            }
            else
            {
                //MOVE BACK TO ORIGINAL SPOT
                moved = false;
				untouched = true;
				dragging = false;
                gameObject.transform.localScale = originalSize;
                gameObject.transform.position = originalPos;
            }
        }
        else if (gameObject.tag == "shooter") 
        {
			tutorialText.text = "";
			tutorialText2.text = "";

            bool infected = tb.activate(point);
            if (!infected)
            {
                moved = false;
				untouched = true;
                dragging = false;
                gameObject.transform.localScale = originalSize;
                gameObject.transform.position = originalPos;
            }
            else
            {
                Debug.Log("this happend");
                Destroy(gameObject);
            }
        }
        else if (gameObject.tag == "meteor")
        {
			tutorialText.text = "";
			tutorialText2.text = "";

            Destroy(gameObject);
            Instantiate(meteorExplosion, point, Quaternion.identity);
            //point.z = 2.0f;
            //Instantiate(meteorVisualization, point, Quaternion.identity);
        }
        else if (gameObject.tag == "mine")
        {
			tutorialText.text = "";
			tutorialText2.text = "";


            Destroy(gameObject);
            Instantiate(landMine, point, Quaternion.identity);
            //transform.position = point;
        }
		else if (gameObject.tag == "trafficTrap")
		{
			tutorialText.text = "";
			tutorialText2.text = "";

			Destroy(gameObject);
			trafficController.breakAllLights ();
			//transform.position = point;
		}
       
    }
}