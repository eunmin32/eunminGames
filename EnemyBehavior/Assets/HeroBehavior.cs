using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroBehavior : MonoBehaviour {

    // public EggStatSystem mEggStat = null;
    public float mHeroSpeed = 20f;
    private const float kHeroRotateSpeed = 22f;
    private Rigidbody2D rb;
    private Transform tr;
    public Vector3 directionVector;
    public float directionAngle;
    public float currentSpeed;
    public GameObject ignoreEnemy;
    private bool forward;
    bool onMouse = false;
    private GameObject ui;
    

    // Use this for initialization
    void Start() {
        tr = GetComponent<Transform>();
        Debug.Log("Hero start");
        rb = GetComponent<Rigidbody2D>();
        directionVector = new Vector3(0,1,0);
        forward = true;
        directionAngle = (transform.rotation.eulerAngles.z) + 90;
        rb.velocity = AngleToVector(directionAngle) * mHeroSpeed;
        ui = GameObject.Find("UI");
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        UpdateMotion();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Mpush");
            onMouse = !onMouse;
            if (!onMouse)
            {
                rb.velocity = AngleToVector(directionAngle) * currentSpeed;
            }
            ui.GetComponent<GameInfo>().MouseChange();
        }
        if (onMouse)
        {
            Vector3 _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(_mousePos.x, _mousePos.y, transform.position.z);
            rb.velocity = AngleToVector(directionAngle) * 0;
        }
    }
    private void UpdateVelocity()
    {
        if (forward)
            rb.velocity = currentSpeed * directionVector;
        else
            rb.velocity = -currentSpeed * directionVector;
    }

    private void UpdateMotion() {
        if (!onMouse)
        {
            mHeroSpeed += Input.GetAxis("Vertical");
            transform.position += transform.up * (mHeroSpeed * Time.smoothDeltaTime);

            
        }
        if (Input.GetKey(KeyCode.D))
        {
            //Debug.Log("Dpush");
            rotate(-2);
        }
        if (Input.GetKey(KeyCode.A))
        {
            ////    //Debug.Log("Apush");
            rotate(2);
        }
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COOOOOLLLLISDE");
    }
}
