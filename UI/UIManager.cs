using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Timers;

public class UIManager : SingletonMonoBehaviour<UIManager> {


	[SerializeField]
	GameObject buildPanel;
	[SerializeField]
	GameObject buildOpen;

	[SerializeField]
	Text materialText;
	
	[SerializeField]
	Text timerText;

	[SerializeField]
	Text moveCountText;
	[SerializeField]
	Text turnText;

	[SerializeField]
	AiryUIAnimationManager airyUIAnimationManager;
	public int timerCount;
	int moveCount = 8;
	// MARK: - Mono LifeCycle
	protected override void OnStart(){
		materialText.text = "0";
		timerText.text = timerCount.ToString();
	}

	// MARK: - Timer
	public void timeCount(){
		if (timerCount == 1){
			Debug.Log("Count finish");
			timerText.text = timerCount.ToString();
			TimersManager.ClearTimer(timeCount);
			GameManager.Instance.gameTrigger = false;
			GameManager.Instance.turn++;
			turnText.text = GameManager.Instance.turn.ToString();
			airyUIAnimationManager.ShowMenu();
			return;
		}
		timerCount--;
		timerText.text = timerCount.ToString();
	}

	// MARK: - Start Scene
	public void startButtonClick(){
		SceneManager.LoadScene(1);
	}

	// MARK: - Game Scene
	public void returnStartSceneButtonClick(){
		SceneManager.LoadScene(0);
	}

	public void buildOpenButtonClick(){
		buildPanel.SetActive(true);
		buildOpen.SetActive(false);
	}

	public void buildCloseButtonClick(){
		buildPanel.SetActive(false);
		buildOpen.SetActive(true);
		foreach (var i in Player.Instance.sampleBuildings){
			i.SetActive(false);
		}
		if (GameManager.Instance.gameTrigger){
			Player.Instance.isBuildingMode = false;
			Player.Instance.isBuildingSample = false; 
		}
		
	}

	public void homeButtonClick(){
		if (Player.Instance.isBuildingSample) {return;}
		Player.Instance.isBuildingMode = true;
		Player.Instance.playerDel();
		Player.Instance.isBuildingSample = true; 
		
	}
}
