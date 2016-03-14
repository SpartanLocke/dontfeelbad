using UnityEngine;
using System.Collections;

public class CarMovement : MonoBehaviour {

	public Transform[] wayPointList;

	public int currentWayPoint = 0;
	public bool vertical;
	Transform targetWayPoint;
	public GameObject stopLight;
	public GameObject explosion;
	public DisplayDeathCounter deathCounter;

	public float speed = 1f;

	private int carIntersectPoint = 1;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// check if we have somewere to walk
		if (currentWayPoint < this.wayPointList.Length)
		{
			if (targetWayPoint == null) targetWayPoint = wayPointList[currentWayPoint];
			//Check if at stoplight
			// Probably should change this to a tag on the waypoint later
			if (vertical && transform.position.y == wayPointList [carIntersectPoint].position.y && !stopLight.GetComponent<TrafficLight> ().isGreen ()) {
			} else if (!vertical && transform.position.x == wayPointList [carIntersectPoint].position.x && !stopLight.GetComponent<TrafficLight> ().isGreen ()) {
			} else {
				drive ();
			}
		}
	}

	void drive()
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
				transform.position = wayPointList [currentWayPoint].position;
			}
			targetWayPoint = wayPointList[currentWayPoint];
		}
	}
		
	void OnDestroy() {
		//deathCounter.addScore (1);
	}

	void OnCollisionEnter2D(Collision2D coll){
		//Debug.Log ("Car COLLIDED");
		if (coll.gameObject.tag == "car") {
			Instantiate (explosion, gameObject.transform.position, Quaternion.identity);
			Destroy (gameObject);

		} else {
			if (coll.gameObject.tag == "man") {
				Destroy (coll.gameObject);
			}
		}
	}
}
