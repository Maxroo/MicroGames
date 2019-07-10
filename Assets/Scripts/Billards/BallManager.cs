using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager instance;
    
    [SerializeField]
    private int numberOfBalls;

    
    private void Awake() {
        if(instance != null){
            Destroy(this);
        }
        instance = this;
    }

    public void RemoveBall(){
        numberOfBalls--;
        if(numberOfBalls<=0){
            if(GameManager.instance != null){

                GameManager.instance.SetGameResult(true);
            }
        }
    }
    
}
