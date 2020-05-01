using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manageObjects : MonoBehaviour
{   //Prefab array
    //Location Array
    private bool random = false;
    private GameInfo gameUI;
    public GameObject EnemyPrefab;
    public GameObject WayPointAPrefab;
    public GameObject WayPointBPrefab;
    public GameObject WayPointCPrefab;
    public GameObject WayPointDPrefab;
    public GameObject WayPointEPrefab;
    public GameObject WayPointFPrefab;
    private GameObject[] Prefabs = new GameObject[7];
    private GameObject[] enemyGroup;
    private Vector3[] PrefabsLoc = { new Vector3(8f, 18f, 0), new Vector3(-63f, -46f,0),new Vector3(63f, 42f, 0),
        new Vector3(53f, -40f, 0),new Vector3(30f, -2f,0), new Vector3(-31f, -7f, 0), new Vector3(-65f, 30f, 0) };
    void Start()
    {
        random = false;
        Prefabs[0] = EnemyPrefab;
        Prefabs[1] = WayPointAPrefab;
        Prefabs[2] = WayPointBPrefab;
        Prefabs[3] = WayPointCPrefab;
        Prefabs[4] = WayPointDPrefab;
        Prefabs[5] = WayPointEPrefab;
        Prefabs[6] = WayPointFPrefab;
        gameUI = GameObject.Find("UI").GetComponent<GameInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyGroup = GameObject.FindGameObjectsWithTag("Enemy");
        int Enumber = enemyGroup.Length;

        if (Enumber < 10)
        {
            respawnEnemy();
        }
        for (int i = 1; i < 7; i++)
        {
            if (GameObject.Find(Prefabs[i].name + "(Clone)") == null)
            {
                respawn(i);
                enemyGroup = GameObject.FindGameObjectsWithTag("Enemy");

            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            flipRandom();
            gameUI.RandomChange();

        }

    }

    Vector3 vectorAround(Vector3 vector, float range)
    {
        Vector3 a = vector + new Vector3(Random.Range(-range, range), Random.Range(-range, range),
        Random.Range(0.0f, 10.0f));
        return a;
    }

    void respawn(int i)
    {          
            Instantiate(Prefabs[i], vectorAround(PrefabsLoc[i], 15), Quaternion.identity);     
    }
    void respawnEnemy()
    {

        Instantiate(Prefabs[0], vectorAround(PrefabsLoc[0], 50), Quaternion.identity);

    }
    private void flipRandom()
    {
        random = !random;
    }

    public bool IsRandom()
    {
        return random;
    }

    public void EnemyDead()
    {
        gameUI.touchEnemy();
    }
}

