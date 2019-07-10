using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableCircle : MonoBehaviour
{
    Vector2 targetPosition;
    Vector3 startingPosition;
    bool isTouchOver;
    private Rigidbody2D rbRef;
    public float maxLeft;
    public float maxRight;
    public GameObject explosion;


    private void Awake() {
        rbRef = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        startingPosition = transform.position;
    }


    void OnTouchDown(){
        isTouchOver = true;

    }
     void OnTouchStay(Vector2 position){
         isTouchOver = true;
         targetPosition = position;
        
    }
     void OnTouchExit(){
        isTouchOver = false;
    }
     void OnTouchUp(){
        isTouchOver = false;
        
    }

    private void Update() {
        if(isTouchOver){

            targetPosition = new Vector2(Mathf.Clamp(targetPosition.x, maxLeft, maxRight), transform.position.y);
            rbRef.MovePosition(Vector2.Lerp(transform.position, targetPosition, 0.3f));
        }

        if(!isTouchOver){
            targetPosition = startingPosition;
            rbRef.MovePosition(Vector2.Lerp(transform.position, targetPosition, 0.3f));
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            Instantiate(explosion, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            GameManager.instance.SetGameResult(false);
        }
    }
}
