using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlugMovement : MonoBehaviour
{

    CharacterController charC;
    public int plugID;
    public int speed;
    Vector3 desiredCameraPosition;
    Vector3 offset;
    private bool ableToMove;
    private bool isStopped;
    Rigidbody rbRef;
    AudioSource audioRef;


    private void Awake() {
        rbRef = GetComponent<Rigidbody>();
        charC = GetComponent<CharacterController>();
        offset = Camera.main.transform.position - transform.position;
        GameManager.OnGameStart += CustomStart;
        audioRef = GetComponent<AudioSource>();
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName(GameManager.instance.currentGameId));

    }

    public void CustomStart(){
        ableToMove = true;
        GameManager.OnGameStart -= CustomStart;
    }
    
    void Update()
    {   
        if(Input.touchCount > 0){
            isStopped = true;
            rbRef.velocity = Vector3.zero;
            
        } else {
            isStopped = false;

        }

        if(ableToMove){
           if(!isStopped){

                Vector3 input = Input.acceleration;

            //input = Quaternion.Euler(90, 0, 90) * input;
                Vector3 movement = new Vector3(input.x * (speed / 2) ,0 ,  9f * speed * Time.deltaTime);



           

            //print(input);

                charC.SimpleMove(movement);
           } 
        }  
        //transform.Translate(input.x ,0 , Time.deltaTime * (input.y + 10f), Space.World);
        desiredCameraPosition = transform.position + offset;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, desiredCameraPosition, 0.05f);
        
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag != "Finish" && other.gameObject.tag != "Floor" && other.gameObject.tag != "Plug"){
            ableToMove = false;
            rbRef.freezeRotation = false;
            rbRef.useGravity = true;
            audioRef.Play();

        }
        print("Collision!");
    }
}
