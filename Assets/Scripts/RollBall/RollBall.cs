using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBall : MonoBehaviour
{
   Rigidbody2D rbRef;
   bool canMove;
   AudioSource rollAudio;

   private void Awake() {
       rbRef= GetComponent<Rigidbody2D>();
       rollAudio = GetComponent<AudioSource>();
       GameManager.OnGameStart+= CustomStart;
       TimerManager.instance.SetGameDescription("Roll to the Ham");
   }

   void CustomStart(){
       canMove = true;

        GameManager.OnGameStart -= CustomStart;
        rollAudio.Play();
   }

   private void Update() {

       if(canMove){
            Vector3 tiltInput = Input.acceleration;

            tiltInput = Quaternion.Euler(90,0, -90) * tiltInput;

            rbRef.AddForce(new Vector2(-tiltInput.z, tiltInput.x + .4f) * 10);
       }

      
   }

   private void OnCollisionEnter2D(Collision2D other) {
       if(rbRef.velocity.magnitude > 0.5f && canMove == true){
           rollAudio.Play();
       }
   }
}
