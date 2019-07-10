using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        GetComponent<AudioSource>().Play();
    }
}
