using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCar : MonoBehaviour
{

    Rigidbody rbRef;
    Vector3 targetPosition;
    Vector3 startPosition;
    public Transform endPosition;
    bool movingToEnd = true;

    private void Awake() {
        rbRef = GetComponent<Rigidbody>();
        startPosition = transform.position;
        targetPosition = endPosition.position;
    }

    private void Start() {
        StartCoroutine(CarMovement());
    }

    IEnumerator CarMovement(){

        while(true){
            while(!Mathf.Approximately(transform.position.x, targetPosition.x)){
                rbRef.MovePosition(Vector3.MoveTowards(transform.position, targetPosition, 0.25f));
                yield return null;
            }
            if(movingToEnd){
                movingToEnd = false;
                targetPosition = startPosition;
            } else
            {
                movingToEnd = true;
                targetPosition = endPosition.position;
            }
        }
    }
}
