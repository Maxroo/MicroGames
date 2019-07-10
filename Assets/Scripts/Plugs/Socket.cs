using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour {

	public int socketID;
	Transform plugTransform;

	public Vector3 offset;
	public AudioSource audioRef;


	private void Awake() {
		TimerManager.instance.SetGameDescription("Steer and Plug in Correctly");
		audioRef = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Plug"){
			PlugMovement plugRef = other.gameObject.GetComponent<PlugMovement>();
			if(plugRef.plugID == socketID){
				plugRef.enabled = false;
				plugRef.gameObject.GetComponent<BoxCollider>().enabled = false;
				audioRef.Play();
				Destroy(plugRef.gameObject.GetComponent<Rigidbody>());				
				plugTransform = other.gameObject.transform;
				StartCoroutine(PlugIn());

			}
		}
	}



	IEnumerator PlugIn(){

		for(int i = 0; i <60; i++){
			plugTransform.position = Vector3.Lerp(plugTransform.position, transform.position + offset, 0.1f);
			plugTransform.rotation = Quaternion.Lerp(plugTransform.rotation, Quaternion.Euler(-180,90,90), 0.1f);
			yield return null;

		}

		GameManager.instance.SetGameResult(true);
	}

	
}
