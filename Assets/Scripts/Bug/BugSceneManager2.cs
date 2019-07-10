using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BugSceneManager2: MonoBehaviour {

	 public static BugSceneManager2 instance = null;
	public GameObject bugobject;
	private Vector3 range; 
	private Vector3 range1; 




	private static bool winningstate = false;

	public List<GameObject> bug = new List<GameObject>();

		public AudioSource AS;
	public AudioClip bgm;

	void customStart()
	{
		
		AS.clip = bgm;
		AS.loop = true;
		AS.Play();
	range = new Vector3(Random.Range(2,6),Random.Range(-2.6f,2.6f),-5f);
	range1 = new Vector3(Random.Range(2,6),Random.Range(-2.6f,2.6f),-5f);


		Invoke("instantiateobject", 1.0f);
		
		GameManager.OnGameStart -= customStart;		

	}


	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
	}
	
	void Update () {
		checkifwin();
		
	}

	void Awake()
	{
            if (instance == null)
                {
                instance = this;
				}
            else if (instance != this)
                {
                Destroy(gameObject);    
				}
            DontDestroyOnLoad(gameObject);
		GameManager.OnGameStart += customStart;
		TimerManager.instance.SetGameDescription("Kill");

	}

	void instantiateobject()
	{
		GameObject current = Instantiate(bugobject,range,Quaternion.identity);
  		bug.Add(current);
		SceneManager.MoveGameObjectToScene(current, SceneManager.GetSceneByName(GameManager.instance.currentGameId));
		current = Instantiate(bugobject,range1,Quaternion.identity);
		SceneManager.MoveGameObjectToScene(current, SceneManager.GetSceneByName(GameManager.instance.currentGameId));
  		bug.Add(current);
	}

	public void checkifwin()
	{
			if (bug.Count == 0)
		{
			print(true);
			GameManager.instance.SetGameResult(true);
		}
	}

}
