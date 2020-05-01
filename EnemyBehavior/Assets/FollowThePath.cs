using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThePath : MonoBehaviour {

    private GameObject[] waypoints = new GameObject[7];
    // Walk speed that can be set in Inspector
    [SerializeField]
    private float moveSpeed = 20f;

    // Index of current waypoint from which Enemy walks
    // to the next one
    private int waypointIndex = 0;

    // Use this for initialization
    private void Start()
    {
        setWayPoint();
        // Set position of Enemy as position of the first waypoint

    }

    // Update is called once per frame
    private void Update()
    {
      //  

        // Move Enemy
        Move();
    }

    public void setWayPoint()
    {
        waypoints[0] = GameObject.Find("A_Walk(Clone)");
        waypoints[1] = GameObject.Find("B_Walk(Clone)");
        waypoints[2] = GameObject.Find("C_Walk(Clone)");
        waypoints[3] = GameObject.Find("D_Walk(Clone)");
        waypoints[4] = GameObject.Find("E_Walk(Clone)");
        waypoints[5] = GameObject.Find("F_Walk(Clone)");
    }
    // Method that actually make Enemy walk
    private void Move()
    {
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
            Debug.Log(waypoints[waypointIndex].name);

            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector3.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                Debug.Log(waypoints[waypointIndex].name);
                waypointIndex += 1;
            }
        if (waypointIndex == 6)
            waypointIndex = 0;
    }
}
