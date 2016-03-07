using UnityEngine;
using System.Collections;

public class CarMovement : MonoBehaviour {

	public Transform[] wayPointList;

	public int currentWayPoint = 0;
	public bool vertical;
	Transform targetWayPoint;
	public GameObject stopLight;
	public GameObject explosion;

	public float speed = 1f;

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
			if (vertical && transform.position.y == wayPointList [1].position.y && !stopLight.GetComponent<TrafficLight> ().isGreen ()) {
			} else if (!vertical && transform.position.x == wayPointList [1].position.x && !stopLight.GetComponent<TrafficLight> ().isGreen ()) {
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

	void OnTriggerEnter2D(Collider2D other) {
		// For now explode on contact - TODO: later check type of collision
		Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
		Destroy (gameObject);

	}
}
