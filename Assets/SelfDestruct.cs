using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    private void Awake() {

        Invoke("Hide", 0.3f);
        Destroy(gameObject, 3f);
    }

    public void Hide(){
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
