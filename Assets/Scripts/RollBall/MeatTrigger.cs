using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatTrigger : MonoBehaviour
{

  public AudioSource meatCongrats;
  private void OnTriggerEnter2D(Collider2D other) {
      if(other.gameObject.tag == "Player"){
        meatCongrats.Play();
        GameManager.instance.SetGameResult(true);
        Destroy(gameObject);
      }
  }
}
