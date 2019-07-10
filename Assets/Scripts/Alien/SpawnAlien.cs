using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnAlien : MonoBehaviour
{

    public GameObject human;
    private static GameObject theAlien;

    public GameObject alien;
    private SpriteRenderer spriteR;
    private int pointlocation;
    private Quaternion rPoint1 = Quaternion.Euler(0,-38.68f,0);
    private float spawntime1,spawntime2,spawntime3,spawntime4,spawntime5,spawntime6;
    public GameObject target;

    private int currentNumber;

    private int layer = 0;

    Walking walking;

    void Awake()
    {
        
        AlienSceneManager.StartThis += customStart;
        
        spawntime1 = UnityEngine.Random.Range(0,2f);
        spawntime2 = UnityEngine.Random.Range(3f,4f);
        spawntime3 = UnityEngine.Random.Range(4f,5f);
        spawntime4 = UnityEngine.Random.Range(5,6f);
        spawntime5 = UnityEngine.Random.Range(6.5f,8f);
        spawntime6 = UnityEngine.Random.Range(7.3f,9f);

    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
      
    }
    
    void customStart()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(GameManager.instance.currentGameId));
        int difficulty = AlienSceneManager.instance.levelDifficulty;

        string[] thisname = gameObject.name.Split(',');
        pointlocation = int.Parse(thisname[1]);
        print(difficulty);


        switch(difficulty)
        {
            case 1:
            case 2:
            switch (pointlocation)
            {
                case 1:
                currentNumber = 1;
                break;
                case 2:
                currentNumber = 6;
                break;
                case 3:
                currentNumber = 11;
                break;
            }
            Invoke("instantiate", spawntime1);
            Invoke("instantiate", spawntime2);
            Invoke("instantiate", spawntime3);
            Invoke("instantiate", spawntime4);
            Invoke("instantiate", spawntime5);
            break;
            case 3:
            
            switch (pointlocation)
            {
            case 1:
                currentNumber = 1;
                break;
            case 2:
                currentNumber = 7;
                break;
            case 3:
                currentNumber = 13;
                break;
            }
            Invoke("instantiate", spawntime1);
            Invoke("instantiate", spawntime2);
            Invoke("instantiate", spawntime3);
            Invoke("instantiate", spawntime4);
            Invoke("instantiate", spawntime5);
            Invoke("instantiate", spawntime6);
            break;
        }
        
      
        AlienSceneManager.StartThis -= customStart;

    }

    void Update()
    {
    }

    void instantiate()
    {   
        GameObject current = null;
        int alienPosition = AlienSceneManager.instance.spawnAlien;
        
       switch (pointlocation)
       {

           case 1:
            if(currentNumber == alienPosition)
            {
            current = Instantiate(alien,transform.position,rPoint1);  
            theAlien = current;       
            }  else
            {
            current = Instantiate(human,transform.position,rPoint1);                
            }
            walking = current.GetComponent<Walking>();
            walking.target = target;
            spriteR = current.GetComponent<SpriteRenderer>();
            spriteR.sortingOrder = layer;
            break;
            case 2:
            if(currentNumber == alienPosition)
            {
            current = Instantiate(alien,transform.position,rPoint1);  
            theAlien = current;              

            }  else
            {
            current = Instantiate(human,transform.position,rPoint1);                
            }
            current.transform.Rotate(0,180,0);
            walking = current.GetComponent<Walking>();
            walking.target = target;
            spriteR = current.GetComponent<SpriteRenderer>();
            spriteR.sortingOrder = layer;
            break;

            case 3:
            if(currentNumber == alienPosition)
            {
            current = Instantiate(alien,transform.position,rPoint1);    
            theAlien = current;              
            }  else
            {
            current = Instantiate(human,transform.position,rPoint1);                
            }
            walking = current.GetComponent<Walking>();
            walking.target = target;
            spriteR = current.GetComponent<SpriteRenderer>();
            spriteR.sortingOrder = layer;
            break;
            
       }

       if(theAlien)
       {
           walking.isAlien = true;
       }else
       {
       AlienSceneManager.instance.peoples.Add(current);           
       }
       theAlien = null;
       currentNumber ++;
        layer --;
    }

}
