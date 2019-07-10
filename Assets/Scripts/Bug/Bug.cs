using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour {

    private string intro = "Kill";
	Vector3 position;
	Vector3 position1;
	Vector3 position2;
	Vector3 position3;
	Vector3 targetposition;
	GameObject gamemanager;
	bool leave = false;
	public Animator animator;
	public float speed;
	private float defualtspeed;
	// Use this for initialization
	private float time = 0f;

	[Range(1.6f, 2)]public float stopa;
	
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{

		animator = GetComponent<Animator>();
		List<int> exitx = new List<int>();
		exitx.Add(-10);
		exitx.Add(10);
		List<int> exity = new List<int>();
		exity.Add(-10);
		exity.Add(10);
		
		defualtspeed = speed;

		position1 = new Vector3(Random.Range(-6,6),Random.Range(-6,6),-5);
		position2 = new Vector3(Random.Range(-6,6),Random.Range(-6,6),-5);
		position3 = new Vector3(Random.Range(-10,10),exity[Random.Range(0,2)],-5);
		print(position3);
		stopa = 2;


		
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetFloat("Speed",speed);
		time += Time.deltaTime;
		position = this.transform.position;
		dir();
		
		if (time <= 1.5){
			targetposition = position1;
			this.transform.position = Vector3.MoveTowards(position,position1, speed * Time.deltaTime);			
		}else if (time > 1.5 && time < stopa){
			speed = 0;
		}else if (time > stopa)
		{
			targetposition = position2;
			speed = defualtspeed;
			if (position == targetposition){
				leave = true;
			}
			if (!leave){
			this.transform.position = Vector3.MoveTowards(position,position2, speed * Time.deltaTime);				
			}else{
			targetposition = position3;
			this.transform.position = Vector3.MoveTowards(position,position3, speed * Time.deltaTime);
			if (position.y > 8.9 || position.y < -8.9 ){
				if (transform.localScale.x > 0)
				{
				transform.localScale -= new Vector3(0.2f,0.2f,0);					
				}
			}
			}
	
		}
	
	
	}
	void dir(){
        transform.rotation = Quaternion.LookRotation(Vector3.back, targetposition - transform.position);
		transform.Rotate(0, 0, 180);
		}

		/// <summary>
		/// OnMouseDown is called when the user has pressed the mouse button while
		/// over the GUIElement or Collider.
		/// </summary>
		void OnMouseDown()
		{
			Destroy(gameObject);
		}
		
		/// <summary>
		/// This function is called when the MonoBehaviour will be destroyed.
		/// </summary>
		void OnDestroy()
		{
			if(BugSceneManager.instance)
			{
			BugSceneManager.instance.bug.Remove(gameObject);
			}else if (BugSceneManager2.instance)
			{
			BugSceneManager2.instance.bug.Remove(gameObject);
				
			}else if(BugSceneManager3.instance)
			{
			BugSceneManager3.instance.bug.Remove(gameObject);
			}
		}

		/// <summary>
		/// This function is called when the behaviour becomes disabled or inactive.
		/// </summary>
		void OnDisable()
		{
		}

		
}
