using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootEgg : MonoBehaviour
{
    public Transform eggPoint;
    public GameObject eggPrefab;
    public float cooldownTime = 0.2f;
    public float shootingForce = 56f;
    private float nextFireTime = 0;
    //public Slider cooldownSlider;
    public Slider cooldownGauge;
    private bool timeCount = false;
    float timeLeft;

    private void Start()
    {
     //   SetCoolDown();
     //   cooldownSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        cooldownGauge.value = 0;

    }

    //public void ValueChangeCheck()
    //{
    //    cooldownTime = cooldownSlider.value;
    //}

    // Update is called once per frame
    void Update()
    {
        if (timeCount)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                cooldownGauge.value = timeLeft / cooldownGauge.maxValue;
            }
            else
            {
                timeCount = false;

            }
        }

    }

    private void FixedUpdate()
    {
        if (Time.time > nextFireTime)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Shoot();
                nextFireTime = Time.time + cooldownTime;
            }
        }
    }

    void Shoot()
    {
        GameObject egg = Instantiate(eggPrefab, eggPoint.position, eggPoint.rotation);
        Rigidbody2D rb = egg.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(egg.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        rb.velocity = eggPoint.up * shootingForce;
        cooldownGauge.maxValue = cooldownTime;
        startTimer();
    }

    //public void SetCoolDown()
    //{
    //    cooldownSlider.maxValue = cooldownTime;
    //    cooldownSlider.value = cooldownTime;
    //}


    void startTimer()
    {
        cooldownGauge.value = cooldownGauge.maxValue;
        timeCount = true;
        timeLeft = cooldownGauge.maxValue;
    }

}
