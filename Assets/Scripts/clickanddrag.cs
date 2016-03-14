﻿using UnityEngine;
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
	public DisplayDeathCounter deathCounter;
    private Vector3 originalSize;
    private Vector3 originalPos;
    public Vector3 point;
    private bool moved = false;
	private bool dragging = false;

	private Color oldColor;


    void Start()
    {
        originalPos = gameObject.transform.position;
		oldColor = gameObject.GetComponent<SpriteRenderer> ().color;
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
		if (gameObject.tag == "meteor" || gameObject.tag == "trafficTrap") {
			//for now, make it green whereever
			//TODO, limit by where i can place the meteor
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (0, 128, 0, .75f);
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
		Debug.Log (other.gameObject.name);
		if (moved) {
			if (other.gameObject.tag != "explosion") {
				dragging = true;
				//Debug.Log ("tag is: " + other.gameObject.tag);
				if (gameObject.tag != "meteor") {
					if (other.gameObject.tag == "man" || other.gameObject.tag == "car" && gameObject.tag == "mine") {
						//gameObject.GetComponent<SpriteRenderer> ().color = new Color(0,20,0,.75f);
						gameObject.transform.localScale = originalSize * scale * 2;
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
	}

	void OnTriggerEnter2D() {
		//Debug.Log("trigger");
	}

    void OnMouseUp()
    {
        moved = true;
        UnityEngine.Cursor.visible = true;

        if (gameObject.tag == "heartattack") //TODO: move this logic outside
        {
            bool killed = ha.activate(point);
            if (killed)
            {
                Destroy(gameObject);


            }
            else
            {
                //MOVE BACK TO ORIGINAL SPOT
                moved = false;
				dragging = false;
                gameObject.transform.localScale = originalSize;
                gameObject.transform.position = originalPos;
            }
        }
        else if (gameObject.tag == "shooter") 
        {
            bool infected = tb.activate(point);
            if (!infected)
            {
                moved = false;
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
            Destroy(gameObject);
            Instantiate(meteorExplosion, point, Quaternion.identity);
            //point.z = 2.0f;
            //Instantiate(meteorVisualization, point, Quaternion.identity);
        }
        else if (gameObject.tag == "mine")
        {

            Destroy(gameObject);
            Instantiate(landMine, point, Quaternion.identity);
            //transform.position = point;
        }
		else if (gameObject.tag == "trafficTrap")
		{

			Destroy(gameObject);
			trafficController.breakAllLights ();
			//transform.position = point;
		}
       
    }
}