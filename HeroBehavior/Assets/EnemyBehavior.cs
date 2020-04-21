using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{

    // public EggStatSystem mEggStat = null;
    public float mHeroSpeed = 30f;
    private const float kHeroRotateSpeed = 22f;
    private Rigidbody2D rb;
    private Transform tr;
    public Vector3 directionVector;
    public float directionAngle;
    public float currentSpeed;

    // Use this for initialization
    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
        directionVector = new Vector3(0, 1, 0);
        directionAngle = (transform.rotation.eulerAngles.z) + 90;
        rb.velocity = AngleToVector(directionAngle) * mHeroSpeed;
        
    }

    private void Update()
    {
        UpdateMotion();
    }
    private void UpdateVelocity()
    {
       
        rb.velocity = currentSpeed * directionVector;

    }



    private void UpdateMotion()
    {
        Vector3 dir = Quaternion.AngleAxis(directionAngle, Vector3.forward) * Vector3.right;
        Vector3 opdir = Quaternion.AngleAxis(directionAngle - 180, Vector3.forward) * Vector3.right;
        currentSpeed = rb.velocity.magnitude;

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
        HBound topBo = collisionData.gameObject.GetComponent<HBound>();

        if (topBo != null)
        {
           
            directionAngle = -directionAngle;
        }
        else
        {
            directionAngle = (180 - directionAngle);
        }
        directionVector = AngleToVector(directionAngle);
        UpdateVelocity();
        rb.rotation = VectorToRotAngle(directionVector);
    }

}
