using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GlobalBehavior : MonoBehaviour {
    public static GlobalBehavior sTheGlobalBehavior = null;
    // public EggStatSystem mEggStat = null;

    public Text mGameStateEcho = null;  // Defined in UnityEngine.UI
   

    //     https://docs.unity3d.com/Manual/ExecutionOrder.html
    void Awake() {
      // just so we know
    }
    
    // Use this for initialization
    void Start () {
        Debug.Log("Global start");
      GlobalBehavior.sTheGlobalBehavior = this;  // Singleton pattern
      
    }
    
    public void DestroyAnEgg() {
    }

    public void UpdateGameState(string msg) {
        mGameStateEcho.text = msg;
    }

}
