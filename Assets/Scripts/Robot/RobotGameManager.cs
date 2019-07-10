using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotGameManager : MonoBehaviour
{

    public GameObject head;
    public GameObject body;
    public GameObject leg;
    public GameObject handL;
    public GameObject handR;

    public GameObject mid;

    public AudioSource AS;

    public AudioClip Celebration;
    public AudioClip Rocket;

    public GameObject background;


    public int levelDifficulty = 1;
    private List<GameObject> allparts = new List<GameObject>();

    private List<GameObject> inGameParts = new List<GameObject>();

    private float spawnTime1,spawnTime2,spawnTime3,spawnTime4,spawnTime5;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        allparts.Add(handL);
        allparts.Add(handR);
        allparts.Add(leg);
        allparts = shuffle(allparts);

        switch(levelDifficulty)
        {
            case 1:
            case 2:
                spawnTime1 = Random.Range(1,2);
                spawnTime2 = Random.Range(2.5f,3.5f);
                spawnTime3 = Random.Range(4f,5f);
                spawnTime4 = Random.Range(5f,6f);
                spawnTime5 = Random.Range(6f,7f);
            break;

            case 3:
                spawnTime1 = Random.Range(1,2);
                spawnTime2 = Random.Range(2f,3f);
                spawnTime3 = Random.Range(3f,4f);
                spawnTime4 = Random.Range(4f,5f);
                spawnTime5 = Random.Range(5.5f,6f);
            break;
        }

        TimerManager.instance.SetGameDescription("Build the Robot");

        GameManager.OnGameStart += customStart;
    }

    // Start is called before the first frame update
    void Start()
    {
        customStart();
    }

    void customStart()
    {
        AS.clip = Rocket;
        AS.Play();
        AS.loop = true;

        StartCoroutine(spawn(allparts[0],spawnTime1));
        StartCoroutine(spawn(allparts[1],spawnTime2));
        StartCoroutine(spawn(allparts[2],spawnTime3));
        StartCoroutine(spawn(allparts[3],spawnTime4));
        StartCoroutine(spawn(allparts[4],spawnTime5)); 
        Invoke("showOff", 12);
        Invoke("sendGameResult", 15);
        GameManager.OnGameStart -= customStart;
    }


    IEnumerator spawn(GameObject part, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        
        spawnParts(part);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnParts(GameObject part)
    {
        GameObject current = Instantiate(part,(new Vector3()), transform.rotation);
        inGameParts.Add(current);
        Parts script = part.GetComponent<Parts>();
    }

    List<GameObject> shuffle(List<GameObject> List)
    {
        List<GameObject> newallparts = new List<GameObject>();
        int n = List.Count;
        newallparts.Add(body);        
        while(n > 0)
        {
            int k = Random.Range(0,n);
            GameObject temp = List[k];
            List.Remove(temp);
            newallparts.Add(temp);
            n = List.Count;        
        }
        newallparts.Add(head);
        return newallparts;
    }

    bool checkIfWin()
    {
        foreach (var item in inGameParts)
        {
        Parts script = item.GetComponent<Parts>();
            if(script.isInRange == false)
            {
                return false;
            }
        }
        return true;
    }

    void sendGameResult()
    {
        AS.Stop();
        if(GameManager.instance != null){

            GameManager.instance.SetGameResult(checkIfWin());
        }
    }

    void showOff()
    {
        AS.Stop();
        AS.loop = false;
        AS.PlayOneShot(Celebration);
        SpriteRenderer sr = background.GetComponent<SpriteRenderer>();
        sr.color = Color.yellow;
        ControlledParts script = mid.GetComponent<ControlledParts>();
        script.gameEnd = true;
        mid.transform.position = new Vector3(0,0,0);
    }
}
