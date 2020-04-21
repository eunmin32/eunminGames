using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public GameObject Shooter;

    private void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Destroy(gameObject, 1.5f);
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
            Debug.Log("egg");      
            Destroy(gameObject);
    }
}
