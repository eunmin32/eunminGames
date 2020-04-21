using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroBehavior : MonoBehaviour {

    // public EggStatSystem mEggStat = null;
    public float mHeroSpeed = 20f;
    private float mHeroForce = 50f;
    private const float kHeroRotateSpeed = 22f;
    private Rigidbody2D rb;
    private Transform tr;
    public Vector3 directionVector;
    public float directionAngle;
    public float currentSpeed;
    public GameObject ignoreEnemy;
    private bool forward;

    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>();
        Debug.Log("Hero start");
        rb = GetComponent<Rigidbody2D>();
        directionVector = new Vector3(0,1,0);
        forward = true;
        directionAngle = (transform.rotation.eulerAngles.z) + 90;
        rb.velocity = AngleToVector(directionAngle) * mHeroSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        UpdateMotion();
    }

    private void Update()
    {
        //currentSpeed = rb.velocity.magnitude;
    }
    private void UpdateVelocity()
    {
        if (forward)
            rb.velocity = currentSpeed * directionVector;
        else
            rb.velocity = -currentSpeed * directionVector;
    }



    private void UpdateMotion() {

        Vector3 dir = Quaternion.AngleAxis(directionAngle, Vector3.forward) * Vector3.right;
        Vector3 opdir = Quaternion.AngleAxis(directionAngle - 180, Vector3.forward) * Vector3.right;
        if (Input.GetKey(KeyCode.W))
        {
            AddForceAtAngle(mHeroForce, directionAngle);
        }
        if (Input.GetKey(KeyCode.S))
        {
            AddForceAtAngle(mHeroForce, directionAngle - 180);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Dpush");
            rotate(-2);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Apush");
            rotate(2);
        }
        currentSpeed = rb.velocity.magnitude;
        forward = ((rb.velocity.x >= 0 && directionVector.x >= 0) || (rb.velocity.x <= 0 && directionVector.x <= 0)) &&
            ((rb.velocity.y >= 0 && directionVector.y >= 0) || (rb.velocity.y <= 0 && directionVector.y <= 0));

        Debug.Log(forward);
       // Debug.Log("rb.velocity: " + rb.velocity + " dir: " + dir);

    }

    public void AddForceAtAngle(float force, float angle)
    {
        rb.AddForce(AngleToVector(angle) * force);
    }

    public void rotate(float angle){
        directionAngle += angle;
        directionVector = AngleToVector(directionAngle);
        UpdateVelocity();
        rb.rotation = VectorToRotAngle(directionVector);

    }

    public Vector3 AngleToVector(float angle)
    {
        angle *= Mathf.Deg2Rad;
        float xComponent = Mathf.Cos(angle);
        float yComponent = Mathf.Sin(angle);
        Vector3 dirVector = new Vector3(xComponent, yComponent, 0);
        return dirVector;
    }

    public float VectorToRotAngle(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;   
        return angle;
    }


    private void OnCollisionEnter2D(Collision2D collisionData)
    {

        Physics2D.IgnoreCollision(ignoreEnemy.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        HBound topBo = collisionData.gameObject.GetComponent<HBound>();
        if (topBo != null)
            directionAngle = -directionAngle;
        else 
            directionAngle = (180 - directionAngle);
        directionVector = AngleToVector(directionAngle);
        UpdateVelocity();
        rb.rotation = VectorToRotAngle(directionVector);
    }

}
