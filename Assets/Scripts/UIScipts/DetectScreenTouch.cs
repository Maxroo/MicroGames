using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectScreenTouch : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(Input.touches.Length > 0){
			SceneManager.LoadScene(1);
		}
	
	}
}
