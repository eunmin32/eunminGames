using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public GameObject Shooter;
    private GameObject ui;


    private void Start()
    {
        
        ui = GameObject.Find("UI");
        ui.GetComponent<GameInfo>().eggThrow();
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {     
        Debug.Log("egg");
        GameObject collidedO = collision.gameObject;
        if (collidedO.layer == 10 || collidedO.layer == 11)
        {
            collidedO.GetComponent<HealthSystem>().Damage();
        } 
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        if (ui != null)
            ui.GetComponent<GameInfo>().eggDied();
        Destroy(gameObject);
    }
}
