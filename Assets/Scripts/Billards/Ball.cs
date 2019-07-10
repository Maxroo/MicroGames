using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector3 target;
    Rigidbody2D rbRef;

    public AudioSource audioRef;
    public AudioClip hitSound;
    public AudioClip ballHitSound;
    public AudioClip pocketSound;



    private void Awake() {
        rbRef = GetComponent<Rigidbody2D>();
    }
    public void CaughtByPocket(Vector3 targetPos){
        target = targetPos;
        StartCoroutine(moveToInside());
    }
    
    // Update is called once per frame
    IEnumerator moveToInside(){

		gameObject.GetComponent<Collider2D>().enabled = false;
        audioRef.PlayOneShot(pocketSound);
		
		for(int i = 0; i<15; i++){
			Vector3 targetVector = new Vector3(target.x, target.y, 0);
			gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetVector,0.2f);
			yield return null;
		}

        BallManager.instance.RemoveBall();
		Destroy(gameObject);

	}

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Floor")){
            audioRef.PlayOneShot(hitSound);
        } else if(other.gameObject.CompareTag("Ball")){
            audioRef.PlayOneShot(ballHitSound);
        }
    }

}
