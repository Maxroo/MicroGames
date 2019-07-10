using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    float rotateSpeed = 1;
    Gyroscope gyro;
    public Vector3 quatModify;
    private Vector3 rotModify = new Vector3(15, 0, 20);
   
    public List<GameObject> objects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        gyro = Input.gyro;
        gyro.enabled = true;
        
    }


    void Awake()
    {
        GameManager.OnGameStart += customStart;     
        TimerManager.instance.SetGameDescription("Empty the Bottle");   
    }
    void customStart()
    {
        Invoke("sendGameResult", 10f);
        GameManager.OnGameStart -= customStart;
        gyro = Input.gyro;
        gyro.enabled = true;

        foreach (GameObject o in objects)
        {
            o.GetComponent<Rigidbody>().useGravity = true;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
        
        transform.Rotate(0, 0,rotateSpeed);
        }   

        if(Input.GetKey(KeyCode.LeftArrow))
        {
        transform.Rotate(0, 0,-rotateSpeed);
        }   
        if(Input.GetKey(KeyCode.UpArrow))
        {
        transform.Rotate(rotateSpeed, 0,0);
        }   

        if(Input.GetKey(KeyCode.DownArrow))
        {
        transform.Rotate(-rotateSpeed, 0,0);
        }   

        // Vector3 tiltinput = Input.acceleration;
        // tiltinput = Quaternion.Euler(90, 0, -90) * tiltinput;

        // Vector3 rotationValue = new Vector3(tiltinput.x + 0.5f, 0  ,-tiltinput.z);

        // transform.Rotate(rotationValue * 5);



        Vector3 modifiedGyro = new Vector3(gyro.attitude.eulerAngles.x, gyro.attitude.eulerAngles.z, gyro.attitude.eulerAngles.y);
        transform.eulerAngles = modifiedGyro + rotModify;

       // transform.eulerAngles = new Vector3(transform.eulerAngles.x + quatModify.x, transform.eulerAngles.y + quatModify.y, transform.eulerAngles.z + quatModify.z);


    }

    bool checkIfWin()
    {
        foreach (var item in objects)
        {
            Target target = item.GetComponent<Target>();
            bool isout = target.isOut;
            if(isout == false)
            {
                return false;
            }
        }
        return true;
    }

    void sendGameResult()
    {
        print(checkIfWin());
        GameManager.instance.SetGameResult(checkIfWin());
        
    }


}
