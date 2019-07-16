using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timers;


public class GameManager : SingletonMonoBehaviour<GameManager> {
	[SerializeField]
	Ground myGround;
	public int turn = 0;
	public bool gameTrigger = false;
	public int timerCount;

	// Use this for initialization
	protected override void OnStart(){
		
	}
	public void timerSetting(){
		gameTrigger = true;
		UIManager.Instance.timerCount = timerCount;
        TimersManager.SetLoopableTimer(this, 1f, UIManager.Instance.timeCount);
		TimersManager.SetPaused(UIManager.Instance.timeCount, false);	
	}

	public void showTurnPanel(){
		
	}


	
	// Update is called once per frame
	void Update () {
		
	}
}
