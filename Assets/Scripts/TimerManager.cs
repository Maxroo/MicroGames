using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour {


	public static TimerManager instance;
	public Slider timeSlider;
	public Text timerText;
	public float timeLeft;
	private float timeSpeed = 0;
	public Text gameDescriptionText;

	private void Awake() {
		if(instance!=null){
			instance = this;
		}
		instance = this;
	}

	public void SetGameTimer(int gameLength){

		timeLeft = gameLength;
		timerText.text = timeLeft.ToString();

	}

	public void StartTimer(){
		GameManager.instance.SetGameResult(false);
		gameDescriptionText.CrossFadeAlpha(0, 0.1f, false);
		StartCoroutine(ShowAndFade());

		StartCoroutine(DecreaseTimer());

	}

	IEnumerator DecreaseTimer(){

		float i = 100/timeLeft;
		timeSlider.value = 100;

		while(timeLeft > 0){
			if(GameManager.instance.GetGameResult()){
				timeSpeed = 2;
			} else{
				timeSpeed = 1;
			}
			timeLeft -= Time.deltaTime * timeSpeed;
			timeSlider.value -= (i  * Time.deltaTime) * timeSpeed;
			timerText.text = Mathf.Round(timeLeft + .5f).ToString();
			yield return null;
		}

		GameManager.instance.TimerEnd();
		
	}

	private void OnDestroy() {
		instance = null;
	}


	public void SetGameDescription(string text){
		gameDescriptionText.text = text;
	}


	IEnumerator ShowAndFade(){

		for(int i = 0; i < 90; i++){

			gameDescriptionText.CrossFadeAlpha(1, 1.1f, false);
			yield return null;
		}

		for(int i = 0; i < 60; i++){

			gameDescriptionText.CrossFadeAlpha(0, .5f, false);
			yield return null;
		}


	}
	
}
