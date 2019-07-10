using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	public GameObject dodo;
	private Vector3 range; 

	private static bool winningstate = false;

	private List<GameObject> bug = new List<GameObject>();

	void Start () {
		range = new Vector3(Random.Range(2,6),Random.Range(-2.6f,2.6f),-5f);

		Invoke("instantiateobject", 1.0f);
		
		Invoke("checkifwin", 6);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void instantiateobject()
	{
		GameObject current = Instantiate(dodo,range,Quaternion.identity);
		print(current);
  		bug.Add(current);
	}

	public bool checkifwin()
	{
		if (bug.Count == 0)
		{
			winningstate = true;
			return true;
		}else
		{
			winningstate = false;
			return false;
		}
	}

	public static bool getwinningstate()
	{
		return winningstate;
	}
}
