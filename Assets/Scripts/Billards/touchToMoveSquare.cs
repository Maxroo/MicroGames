using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchToMoveSquare : MonoBehaviour {

	bool moving = false;
	Rigidbody2D rbRef;

	private void Awake() {
		if(TimerManager.instance != null){
			TimerManager.instance.SetGameDescription("Pocket the Balls");

		}
	}
	private void Start() {
		rbRef = GetComponent<Rigidbody2D>();
	}

	private void Update() {
		if(moving){
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			pos.z = 0;
			rbRef.MovePosition(pos);	
		}
	}

	private void OnMouseDrag() {
	
		moving = true;
		
	}
	private void LateUpdate() {
		moving = false;
	}

	private void OnCollisionEnter2D(Collision2D other) {
		
		other.rigidbody.AddRelativeForce(other.relativeVelocity / 1000000);

	}

	
}
