using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBound : MonoBehaviour
{
    private float rightBound;
    private float leftBound;
    private float topBound;
    private float bottomBound;
    private Vector3 pos;
    private Transform target;
    private SpriteRenderer spriteBounds;

    // Use this for initialization
    void Start()
    {
        float vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;
        spriteBounds = GameObject.Find("1 - Background").GetComponentInChildren<SpriteRenderer>();
        target = GameObject.FindWithTag("Player").transform;
        leftBound = (float)(horzExtent - spriteBounds.sprite.bounds.size.x / 2.0f);
        rightBound = (float)(spriteBounds.sprite.bounds.size.x / 2.0f - horzExtent);
        bottomBound = (float)(vertExtent - spriteBounds.sprite.bounds.size.y / 2.0f);
        topBound = (float)(spriteBounds.sprite.bounds.size.y / 2.0f - vertExtent);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log();
        var pos = new Vector3(target.position.x, target.position.y, transform.position.z);
        pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);
        pos.y = Mathf.Clamp(pos.y, bottomBound, topBound);
        transform.position = pos;
    }
    void OnLevelWasLoaded()
    {
        Start();
    }
}
