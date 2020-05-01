using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    private GameObject[] waypoints = new GameObject[7];
    // Walk speed that can be set in Inspector
    private float moveSpeed = 15f;
    // Index of current waypoint from which Enemy walks
    // to the next one
    private int waypointIndex = 0;
    private GameObject gameManager;
    private manageObjects objectManager;
    private bool random = false;
    // Use this for initialization
    private void Start()
    {     
        //while(objectManager == null)
        //{
        gameManager = GameObject.Find("GameManager");
        objectManager = gameManager.GetComponent<manageObjects>();
        //}

        setWayPoint();
        // Set position of Enemy as position of the first waypoint

    }

    // Update is called once per frame
    public void Update()
    {
        if (random != objectManager.IsRandom()) {
            Debug.Log("JJJJJJJJJJJJJJJJJJJJJJ");
            random = !random;
            setWayPoint();
        }
        Move();        
    }
    public void setWayPoint()
    {
        if (random)
        {
            setRandomPoint();
        } else
        {
            setWaySeqeunce();
        }
    }

    public void setWaySeqeunce()
    {
        waypoints[0] = GameObject.Find("A_Walk(Clone)");
        waypoints[1] = GameObject.Find("B_Walk(Clone)");
        waypoints[2] = GameObject.Find("C_Walk(Clone)");
        waypoints[3] = GameObject.Find("D_Walk(Clone)");
        waypoints[4] = GameObject.Find("E_Walk(Clone)");
        waypoints[5] = GameObject.Find("F_Walk(Clone)");
    }

    public void setRandomPoint()
    {
        Shuffle(waypoints);
    }
    // Method that actually make Enemy walk
    private void Move()
    {
        try {
            // If Enemy didn't reach last waypoint it can move
            // If enemy reached last waypoint then it stops

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
                waypointIndex += 1;
            }
            if (waypointIndex == 6)
                waypointIndex = 0;


            Rigidbody2D curr = GetComponent<Rigidbody2D>();
            Vector2 direction = new Vector2(waypoints[waypointIndex].transform.position.x, waypoints[waypointIndex].transform.position.y)
                - curr.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            curr.rotation = angle;
   
            //transform.rotation = Quaternion.Euler(rotationVector);

        } catch
        {
            Debug.Log("catch!");
            setWayPoint();
        }
    }

    public void Shuffle(GameObject[] waypoints)
    {
 
            for (int t = 0; t < waypoints.Length; t++)
            {
                GameObject tmp = waypoints[t];
                int r = Random.Range(t, waypoints.Length);
                waypoints[t] = waypoints[r];
                waypoints[r] = tmp;
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectManager.EnemyDead();
        Destroy(gameObject);
    }


}
