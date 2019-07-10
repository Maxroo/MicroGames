using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class AlienSceneManager : MonoBehaviour
{   
    private string intro = "Find the Alien";
	public static AlienSceneManager instance = null;
    public int spawnAlien;
    public bool isWin = false;
    public int levelDifficulty;

    public AudioSource AS;
    public AudioClip bgm;

    public bool killer = false;    
    public bool clicked = false;

    public List<GameObject> peoples = new List<GameObject>();

    public delegate void StartAction();
    public static event StartAction StartThis;

    void Awake()
    {
         if(instance != null){
			Destroy(this);
		}
		instance = this;
        TimerManager.instance.SetGameDescription(intro);
        string[] sceneDifficulty = Regex.Split(GameManager.instance.currentGameId,".+(\\d)");
        levelDifficulty = int.Parse(sceneDifficulty[1]);
        switch(levelDifficulty)
       {
           case 1:
           case 2:
            spawnAlien = Random.Range(5, 14);
            
           break;
           case 3:
            spawnAlien = Random.Range(6, 17);
           break;
       }

        GameManager.OnGameStart += customStart;
        InvokeRepeating("killerStare",2,1);
        Invoke("setGameResult",10);
    }
    void Start()
    {
        
    }

    void customStart()
    {
        AS.clip = bgm;
        AS.loop = true;
        AS.Play();
        StartThis();
        GameManager.OnGameStart -= customStart;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void killerStare()
    {
        if(killer)
        {
            foreach (GameObject item in peoples)
            {
               Walking walking = item.GetComponent<Walking>();
                walking.speed = 0;
            }
        }
    }
}
