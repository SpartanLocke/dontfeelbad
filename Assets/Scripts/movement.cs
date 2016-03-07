using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour
{

    public Transform[] wayPointList;

    public int currentWayPoint = 0;
    Transform targetWayPoint;
    public float speed = 1f;
	public DisplayDeathCounter deathCounter;

	private TrafficController trafficController;
    // Use this for initialization
    void Start()
    {
		GameObject trafficControl = GameObject.FindWithTag ("trafficController");
		if(trafficControl){
			trafficController = trafficControl.GetComponent<TrafficController> ();
		}
    }

    // Update is called once per frame
    void Update()
    {
        // check if we have somewere to walk
        if (currentWayPoint < this.wayPointList.Length)
        {
            if (targetWayPoint == null) targetWayPoint = wayPointList[currentWayPoint];

			// If we are not at a corner, walk to the next waypoint
			// This assumes people will only cross the street at corners
			// If they cross the street somewhere else they can get hit by a car still
			if (!(wayPointList [currentWayPoint].tag == "corner")) {
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

	void OnDestroy(){
		// TODO: Score depends on person
		deathCounter.addScore (1);
	}
}
