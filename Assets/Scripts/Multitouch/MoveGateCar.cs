using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGateCar : MonoBehaviour
{
    // Start is called before the first frame update
    bool isStarted = false;
    public int speed;

    AudioSource carAudio;

    Rigidbody2D rbRef;
    public Vector3 oldPosition;



    private void Start() {
        isStarted = true;
    }
    void CustomStart(){
        GameManager.OnGameStart -= CustomStart;
        isStarted = true;


    }
    private void Awake() {
        rbRef = GetComponent<Rigidbody2D>();
        carAudio = GetComponent<AudioSource>();
        if(TimerManager.instance != null){

            TimerManager.instance.SetGameDescription("Let in the Red Car");
        }
        GameManager.OnGameStart += CustomStart;
    }

    private void FixedUpdate() {

        if(isStarted){
            //rbRef.MovePosition(Vector2.MoveTowards(transform.position,new Vector2(transform.position.x, transform.position.y + 1), 0.1f));
            rbRef.AddForce(new Vector2(0, 2));
        }

        if(rbRef.velocity.magnitude < 0.05f){
            if(carAudio.isPlaying){
                StartCoroutine(AudioFadeOut.FadeOut(carAudio, 0.5f));
            }
        } else{
            if(!carAudio.isPlaying){
                carAudio.Play();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Finish")){
            if(GameManager.instance != null){
                if(gameObject.CompareTag("Player")){
                    GameManager.instance.SetGameResult(true);
                } else
                {
                    GameManager.instance.SetGameResult(false);
                }
            }
            print("SUCCESS");
        } else if(other.gameObject.CompareTag("Ball")){

            isStarted = false;
            rbRef.velocity = Vector2.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        print("PRINTT");
        isStarted = true;
        
    }
}
