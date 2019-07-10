using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

	public static CanvasManager instance;
	public Image canvasBackground;
	public Slider[] hearts;
	public Text scoreText;
	private Slider heartToLose;
	private int gameTypeInt;
	public CanvasGroup scoreHeartsGroup;
	public CanvasGroup gameTypeGroup;
	public CanvasGroup touchGroup;
	public CanvasGroup tiltGroup;

	public CanvasGroup gameOverCanvas;


	private void Awake() {
		if(instance != null){
			Destroy(this);
		}
		instance = this;
	}

	private void Start() {

	}

	public void showGO(){

		StartCoroutine(FadeInGameOver());

	}

	public void SetScoreText(string text){
		scoreText.text = text;
	}

	public void LooseHeart(int heartNumber){

		heartToLose = hearts[heartNumber - 1];
		StartCoroutine(ReduceHeart());

	}
	public void ShowTilt(){
		tiltGroup.alpha = 1;
		touchGroup.alpha = 0;
		GameTypeFadeIn();

	}
	public void ShowTouch(){
		tiltGroup.alpha = 0;
		touchGroup.alpha = 1;
		GameTypeFadeIn();


	}
	public void ScoreFadeOut(){

		scoreHeartsGroup.alpha = 1;
		StartCoroutine(FadeOutScore());

	}
	public void ScoreFadeIn(){

		scoreHeartsGroup.alpha = 0;
		StartCoroutine(FadeInScore());
	
	}
	public void GameTypeFadeIn(){

		gameTypeGroup.alpha = 0;
		StartCoroutine(FadeInGameType());

	}
	public void GameTypeFadeOut(){

		gameTypeGroup.alpha = 1;
		StartCoroutine(FadeOutGameType());

	}

	public void FadeOutBackground(){
		canvasBackground.CrossFadeAlpha(0, .5f, false);

	}


	public void FadeInBackground(){

		canvasBackground.CrossFadeAlpha(1, 0.5f, false);

	}

	IEnumerator ReduceHeart(){

		while(heartToLose.value > 0){
			heartToLose.value -= 1;
			yield return null;
		}
		
	}
	IEnumerator FadeOutScore(){


		while(scoreHeartsGroup.alpha > 0){
			scoreHeartsGroup.alpha -= 0.05f;
			yield return null;
		}

	}
	IEnumerator FadeInScore(){

		while(scoreHeartsGroup.alpha < 1){
			scoreHeartsGroup.alpha += 0.05f;
			yield return null;
		}

	}
	IEnumerator FadeInGameType(){

		while(gameTypeGroup.alpha < 1){

			gameTypeGroup.alpha += 0.05f;
			yield return null;

		}

		yield return new WaitForSeconds(2);

		FadeOutBackground();

		StartCoroutine(FadeOutGameType());
	}
	IEnumerator FadeOutGameType(){

		while(gameTypeGroup.alpha > 0){

			gameTypeGroup.alpha -= 0.08f;
			yield return null;

		}

	}

	IEnumerator FadeInGameOver(){
		while(gameOverCanvas.alpha < 1){

			gameOverCanvas.alpha += 0.01f;
			yield return null;

		}
	}

}
