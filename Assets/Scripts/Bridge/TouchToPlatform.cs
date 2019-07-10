using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchToPlatform : MonoBehaviour {

	[SerializeField]
	PathfindingGrid gridRef;
	public GameObject platformRef;

	public AudioSource audioRef;
	public AudioClip clip;

	void Update () {
		if(Input.touchCount > 0){
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Node current = gridRef.GridFromWorldPoint(pos);
			print(current.gridX + ", " + current.gridY);
			if(!current.hasFloor){
				current.hasFloor = true;
				audioRef.PlayOneShot(clip);
				GameObject platformSpawned = Instantiate(platformRef,current.worldPosition, Quaternion.identity);
				SceneManager.MoveGameObjectToScene(platformSpawned,SceneManager.GetSceneByName(GameManager.instance.currentGameId));
				print("here");

			}
		}
		
	}
}
