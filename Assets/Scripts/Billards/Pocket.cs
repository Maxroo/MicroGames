using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour {

	Ball currentBall;

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Ball")){
			other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			Ball currentBall = other.gameObject.GetComponent<Ball>();
			currentBall.CaughtByPocket(transform.position);
		}
	}
}
