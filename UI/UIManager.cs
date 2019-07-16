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

	// MARK: - Mono LifeCycle
	protected override void OnStart(){
		materialText.text = "0";
		timerText.text = timerCount.ToString();
	}

	// MARK: - Timer
	public void timeCount(){
		if (timerCount == 0){
			Debug.Log("Count finish");
			timerText.text = GameManager.Instance.timerCount.ToString();
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
	public void moveCountChange(int count){
		moveCountText.text = count.ToString();
	}
	public void materialCountChange(int count){
		materialText.text = count.ToString();
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
		Player.Instance.isBuildingMode = false;
		Player.Instance.isBuildingSample = false; 
		Player.Instance.playerDel();
	}

	public void homeButtonClick(){
		if (!GameManager.Instance.gameTrigger) {return;}
		if (Player.Instance.isBuildingSample) {return;}
		Player.Instance.isBuildingMode = true;
		Player.Instance.playerDel();
		Player.Instance.isBuildingSample = true; 
		
	}
}
