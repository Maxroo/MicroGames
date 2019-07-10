using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearioGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private string intro = "Jump";

    public GameObject Goomba;

    public static BearioGameManager instance;

    public GameObject BackGorund;
    public GameObject BackGorund1;

    public GameObject BackGorund2;
    public AudioSource AS;


    public int level;
    public bool isdead = false;

    public AudioClip BGM;

    float speed = -0.1f;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        instance = this;

        TimerManager.instance.SetGameDescription(intro);

        GameManager.OnGameStart += customStart;
        

    }
    void Start()
    {
        customStart();
    }

    void customStart()
    {
        AS.clip = BGM;
        AS.Play();

         switch(BearioGameManager.instance.level)
        {
            case 1:
            case 2:
        InvokeRepeating("callsapwn",2f,2f);
            break;
            case 3:
        InvokeRepeating("callsapwn",2f,1f);
            break;
        }
        Invoke("sendGameResult", 9f);
        GameManager.OnGameStart -= customStart;

    }

    // Update is called once per frame
    void Update()
    {

        if(isdead)
        {
            AS.Stop();
        }
        Vector3 current = BackGorund.transform.position; 

        Vector3 current1 = BackGorund1.transform.position; 

         if(checkPostion(BackGorund))
        {
        BackGorund.transform.position = new Vector3(current.x + speed,current.y, 0);            
            
        }else
        {
            Vector3 currentt = BackGorund.transform.position; 
            BackGorund.transform.position = new Vector3(22.58f,currentt.y,0);
        }


        if(checkPostion(BackGorund1))
        {
        BackGorund1.transform.position = new Vector3(current1.x + speed,current1.y, 0);            
            
        }else
        {
            Vector3 currentt = BackGorund1.transform.position; 
            BackGorund1.transform.position = new Vector3(22.58f,currentt.y,0);
        }
        
        Vector3 current2 = BackGorund2.transform.position; 

        if(checkPostion(BackGorund2))
        {
        BackGorund2.transform.position = new Vector3(current2.x + speed,current2.y, 0);            
            
        }else
        {
            Vector3 currentt = BackGorund2.transform.position; 
            BackGorund2.transform.position = new Vector3(22.58f,currentt.y,0);
        }
    }


    bool checkPostion(GameObject gameobject)
    {
        SpriteRenderer SP;
        SP = gameobject.GetComponent<SpriteRenderer>();
        if(gameobject.transform.position.x < -19.14)
        {
            SP.renderingLayerMask --;
            return false;
        }
        return true;
    }

    void callsapwn()
    {
        Invoke("spawn", Random.Range(2f,2.5f));
    }
    void spawn()
    {
        Instantiate(Goomba,new Vector3(9.92f,-3,0),transform.rotation);
    }

      bool checkIfWin()
    {
        return !isdead;
    }

    void sendGameResult()
    {
        GameManager.instance.SetGameResult(checkIfWin());
    }
}
