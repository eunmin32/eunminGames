using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {
    private float health;
    private bool hide = false;
    private Color colorO;
    // Start is called before the first frame update
    private void Start()
    {
        SpriteRenderer Y = gameObject.GetComponent<SpriteRenderer>();
        colorO = Y.color;
        health = 4f;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (gameObject.layer == 10) {
                hide = !hide;
            }
            if (hide)
            {
                Debug.Log("Hide");
                Hide();
            }
            else
            {
                Debug.Log("Find");

                Find();
            }
        }
    }


    public HealthSystem(float health){
        this.health = health;
    }

    public float GetHealth()
    {
        return health;
    }

    public void Damage()
    {
        health--;
        if(health == 0f)
        {
            Destroy(gameObject);
        }
        if (!hide)
            Find();
    }

    public void Hide()
    {
        SpriteRenderer Y = gameObject.GetComponent<SpriteRenderer>();   
        colorO.a = 0;
        Y.color = colorO;
    }

    public void Find()
    {
        SpriteRenderer Y = gameObject.GetComponent<SpriteRenderer>();
        var colorO = Y.color;
        colorO.a = health * 0.25f;
        Y.color = colorO;
    }
}
