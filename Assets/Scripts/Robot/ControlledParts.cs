using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledParts : MonoBehaviour
{
    public bool gameEnd = false;
    public bool gameStarted = false;
    public Collider2D collider;
   
    private void Awake() {
        GameManager.OnGameStart += CustomStart;
    }

    public void CustomStart(){
        gameStarted = true;
    }
    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        if(!gameEnd && gameStarted)
        {

        Vector3 tiltinput = Input.acceleration;
        tiltinput = Quaternion.Euler(90, 0, -90) * tiltinput;
        transform.Translate(new Vector3(-tiltinput.z, tiltinput.x + 0.5f, 0) / 3);
        }

        if (transform.position.x <= -8.37f)
        {
         transform.position = new Vector3(-8.37f, transform.position.y, transform.position.z);

        }
        else if (transform.position.x >= 8.19f)
        {
            transform.position = new Vector3(8.19f, transform.position.y, transform.position.z);

        }  

        if (transform.position.y <= -5.34f)
        {
            transform.position = new Vector3(transform.position.x, -5.34f, transform.position.z);
        }
        else if (transform.position.y >= 4f)
        {
            transform.position = new Vector3(transform.position.x, 4, transform.position.z);

        }
    
    }
}
