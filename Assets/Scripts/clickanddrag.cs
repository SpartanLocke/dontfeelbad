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
	public DisplayDeathCounter deathCounter;
    private Vector3 originalSize;
    private Vector3 originalPos;
    public Vector3 point;
    private bool moved = false;


    void Start()
    {
        originalPos = gameObject.transform.position;
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
                deathCounter.addScore(1);
                Destroy(gameObject);


            }
            else
            {
                //MOVE BACK TO ORIGINAL SPOT
                moved = false;
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
                gameObject.transform.localScale = originalSize;
                gameObject.transform.position = originalPos;
            }
            else
            {
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

			//Destroy(gameObject);
			trafficController.breakAllLights ();
			//transform.position = point;
		}
       
    }
}