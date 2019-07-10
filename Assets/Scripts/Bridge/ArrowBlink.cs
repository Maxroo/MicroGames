using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBlink : MonoBehaviour
{
    

    SpriteRenderer renderRef;

    private void Awake() {
        renderRef = GetComponent<SpriteRenderer>();
        GameManager.OnGameStart += CustomStart;
    }
  

    void CustomStart(){
        StartCoroutine(BlinkArrow());
        GameManager.OnGameStart -= CustomStart;
    }
    
    IEnumerator BlinkArrow(){

        yield return new WaitForSeconds(0.3f);

        for(int i = 0; i < 5; i++){
            yield return new WaitForSeconds(0.2f);
            renderRef.enabled = !renderRef.enabled;


        }


    }
}
