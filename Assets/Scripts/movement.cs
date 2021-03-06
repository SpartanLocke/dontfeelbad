﻿using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour
{

    public Transform[] wayPointList;

    public int currentWayPoint = 0;
    Transform targetWayPoint;
    public float speed = 1f;
	public DisplayDeathCounter deathCounter;

    public bool attracted = false;
    private GameObject attractiveMan;
    public bool scared = false;
    private GameObject scaryMan;
    private GameObject target;

    public float killRange = .1f;
    public float gunRange = 2.0f;
    public bool gun;
    public float gunProb = .3f;
    public bool berserk = false;
    public float berserkSpeed = .5f;
    public float scaredSpeed = .3f;
    public float safeDist = 0.0001f;
    public float rRange;
    public float leftOrRight;
    

    public BoxCollider2D physics;
    public CircleCollider2D sight;

    private TrafficController trafficController;
    // Use this for initialization
    void Start()
    {
        leftOrRight = Random.value;
        
    	Physics2D.IgnoreCollision(sight, GameObject.FindWithTag("car").GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(physics, GameObject.FindWithTag("man").GetComponent<Collider2D>());
        giveGun();
        GameObject trafficControl = GameObject.FindWithTag ("trafficController");
		if( trafficControl != null ){
			trafficController = trafficControl.GetComponent<TrafficController> ();
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (berserk && target != null)
        {
            hunt();
        }
        else if (scared)
        {
            checkDist();
            walkAway();
        }
        else if (attracted)
        {

        }

        // check if we have somewere to walk
        else if (currentWayPoint < this.wayPointList.Length)
        {
            if (targetWayPoint == null) targetWayPoint = wayPointList[currentWayPoint];

			// If we are not at a corner, walk to the next waypoint
			// This assumes people will only cross the street at corners
			// If they cross the street somewhere else they can get hit by a car still
			if (!(wayPointList [currentWayPoint].tag == "corner")||targetWayPoint.tag != "corner") {
				walk ();
			}
			else{
				// Don't check for crossing until person actually reaches corner
				if (Vector3.Distance(gameObject.transform.position,wayPointList [currentWayPoint].transform.position) <0.2) {
					// Corner Waypoints must be aligned with the same x and y values
					//Debug.Log("Checking Crossing");
					if (wayPointList [currentWayPoint].transform.position.x == wayPointList [(currentWayPoint +1)%wayPointList.Length].transform.position.x) {
						if (trafficController.verticalIsGreen ()) {
							walk ();
						}
					} else if (wayPointList [currentWayPoint].transform.position.y == wayPointList [(currentWayPoint +1)%wayPointList.Length].transform.position.y) {
						if (trafficController.horizontalIsGreen ()) {
							walk ();
						}
					} else {
						// Other cases such as walking diagonally we just allow them to walk since they arent adhering to traffic rules anyway
						walk ();
					}
				} else {
					walk ();
				}
			}
        }
    }

    void walk()
    {
        // rotate towards the target
        //transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.position - transform.position, speed * Time.deltaTime, 0.0f);

        // move towards the target
        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed * Time.deltaTime);

        if (transform.position == targetWayPoint.position)
        {
            currentWayPoint++;
            if (currentWayPoint >= wayPointList.Length)
            {
                currentWayPoint = 0;
            }
            targetWayPoint = wayPointList[currentWayPoint];
        }
    }

    void giveGun()
    {
        float num = Random.value;
        if (num < gunProb)
        {
            gun = true;
            killRange = gunRange;
        }
        else
        {
            gun = false;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)       //In order for this to work, must turn off isKinematic, must set the mass and gravity to zero, and fix all constraints on the rigidbody
    {
        if (coll.gameObject.tag == "man" && berserk)   //coll.gameObject.tag == "man" && target == null
        {
            coll.gameObject.GetComponent<movement>().setScared(gameObject);
            if (target == null)         //if berserk
            { //make any man i run into while berserk scared of me
                target = coll.gameObject;
                //target.GetComponent<movement>().setScared(gameObject);
            }
        }
    }

    void OnDestroy(){
        if (berserk)
        {
            GameObject[] men = GameObject.FindGameObjectsWithTag("man");
            foreach (GameObject man in men)
            {
                man.GetComponent<movement>().scared = false;
                man.GetComponent<movement>().scaryMan = null;
            }
        }
		deathCounter.addScore (1);
        GameObject.FindGameObjectWithTag("soundManager").GetComponent<soundManager>().wilhelm();
    }

    void hunt()
    {
        Vector3 dist = -transform.position + target.transform.position;
        transform.Translate(berserkSpeed * dist.normalized);
        if (dist.magnitude < killRange)
        {
            if (gun)
            {
                //shoot animation
                Destroy(target);
            }
            else
            {
                //punch animation
                Destroy(target);
            }
            
        }
    }

    void walkAway()
    {
        Vector3 direction = scaredSpeed * (transform.position - scaryMan.transform.position).normalized;
        Vector3 rand = new Vector3(Random.Range(-rRange, rRange), Random.Range(-rRange, rRange),0);
        Quaternion rotation;
        if (leftOrRight < .5f)
        {
            rotation = Quaternion.Euler(0, 0, Random.Range(0, rRange));
        }
        else {
            rotation = Quaternion.Euler(0, 0, Random.Range(-rRange, 0));
        }
        transform.Translate(rotation*direction );
    }

    void walkTowards()
    {
        transform.position = Vector3.MoveTowards(transform.position, attractiveMan.transform.position, speed * Time.deltaTime);
    }

    void checkDist()
    {
        Vector3 dist = gameObject.transform.position - scaryMan.transform.position;
        //Debug.Log(dist.magnitude);
        if (dist.magnitude > safeDist)
        {
            setNormal();
        }
    }

    public void setBerserk()
    {
        berserk = true;
    }

    void setScared(GameObject scary)
    {
        scared = true;
        scaryMan = scary;
    }

    void setNormal()
    {
        scared = false;
        scaryMan = null;
    }

    void setAttracted(GameObject attractive)
    {
        attracted = true;
        attractiveMan = attractive;
    }
}
