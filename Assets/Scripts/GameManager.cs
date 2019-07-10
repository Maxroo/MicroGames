using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public AudioSource AS;
	public AudioClip loseHeart;
	public AudioClip scoreUp;
	private int livesRemaining = 3;

	public int currentScore;

	public int gameDificulty;

	private bool gameResult = false;
	public string currentGameId;

	public enum GameType{
		TOUCH,
		TILT
	}

	public bool isGameOver;

	public GameType currentGameType;

	public delegate void StartAction();
	public static event StartAction OnGameStart;
	public delegate void EndAction();
	public static event EndAction OnTimerEnd;

	public List<String> gamesThisLevel;



	public string[] gameNames;

	private void Awake() {
		if(instance != null){
			Destroy(this);
		}
		instance = this;
		gamesThisLevel = new List<string>(gameNames);
	}

	public bool GetGameResult(){
		return gameResult;
	}

	private void Start() {

		CanvasManager.instance.SetScoreText(currentScore.ToString());
		CanvasManager.instance.ScoreFadeIn();
		StartCoroutine(StartNextGame());

	}

	IEnumerator StartNextGame(){

		int randomGame = UnityEngine.Random.Range(0, gamesThisLevel.Count);

		string[] result = gamesThisLevel[randomGame].Split(',');

		gamesThisLevel.RemoveAt(randomGame);

		int gameTimer = int.Parse(result[1]);
	
		currentGameId = result[0] + gameDificulty;

		yield return new WaitForSeconds(2);

		CanvasManager.instance.ScoreFadeOut();

		if(result[2].Equals("tilt")){

			CanvasManager.instance.ShowTilt();
		} else{
			CanvasManager.instance.ShowTouch();
		}

		SceneManager.LoadScene(2, LoadSceneMode.Additive);
		SceneManager.LoadScene(currentGameId, LoadSceneMode.Additive);

		yield return new WaitForSeconds(2);

		TimerManager.instance.SetGameTimer(gameTimer);
		TimerManager.instance.StartTimer();

		if(OnGameStart != null){
			OnGameStart();
		}

	}

	public void SetGameResult(bool gameEndResult){

		gameResult = gameEndResult;

	}

	public void TimerEnd(){

		StartCoroutine(GameEndTransition());
	
	}

	IEnumerator GameEndTransition(){

		CanvasManager.instance.FadeInBackground();
		yield return new WaitForSeconds(0.5f);
		
		if(OnTimerEnd != null){
			OnTimerEnd();

		}

		CanvasManager.instance.ScoreFadeIn();
		yield return new WaitForSeconds(0.5f);

		if(gameResult){
			currentScore++;
			CanvasManager.instance.SetScoreText(currentScore.ToString());
			AS.PlayOneShot(scoreUp);
			if(currentScore%5==0 && currentScore>0){
				gameDificulty += gameDificulty<3?1:0;
				gamesThisLevel = new List<string>(gameNames);
			}

		} else {
			CanvasManager.instance.LooseHeart(livesRemaining);
			print(livesRemaining);
			AS.PlayOneShot(loseHeart);
			livesRemaining--;
			if(livesRemaining<=0){
				isGameOver = true;
			}
		}

		if(gamesThisLevel.Count<1){
			gamesThisLevel = new List<string>(gameNames);
		}
		
		SceneManager.UnloadSceneAsync(currentGameId);
		SceneManager.UnloadSceneAsync(2);

		yield return null;

		if(!isGameOver){
			StartCoroutine(StartNextGame());
		} else
		{
			StartCoroutine(ShowGameOver());
		}

	}

	IEnumerator ShowGameOver(){
		yield return new WaitForSeconds(2);
		CanvasManager.instance.showGO();
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene("MainMenu");

	}

	
}
