using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour { 
    public Text info;
    public string Random = "Sequence";
    public string Mouse = "Key";
    int touchedEnemy = 0;
    int eggNum = 0;


    // Start is called before the first frame update
    void Start()
    {
        setString();
    }

    // Update is called once per frame

    void setString()
    {
        info.text = "WayPoint:(" + Random + ")  Hero: Drive(" + Mouse + ")  Touched Enemy(" + touchedEnemy + ")   Egg: On Screen(" + eggNum + ")";
        //WayPoint:(Sequence/Random) Hero: Drive(Mouse/Key) Touched Enemy(0) Egg: On Screen(0) 
    }

    public void RandomChange()
    {
        Debug.Log("rangeChange Called");
        if (Random == "Sequence")
            Random = "Random";
        else
            Random = "Sequence";
        setString();
    }

    public void MouseChange()
    {
        if (Mouse == "Key")
            Mouse = "Mouse";
        else
            Mouse = "Key";
        setString();
    }

    public void touchEnemy()
    {
        touchedEnemy++;
        setString();
    }

    public void eggThrow()
    {
        eggNum++;
        setString();
    }

    public void eggDied()
    {
        eggNum--;
        setString();
    }
}
