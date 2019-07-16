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
	public int activeCount;
	
	int m_activeCount;
	// Use this for initialization
	protected override void OnAwake(){
		UIManager.Instance.timerCount = timerCount;
		m_activeCount = activeCount;
	}
	public void timerSetting(){
		gameTrigger = true;
		activeCount = m_activeCount;
		UIManager.Instance.moveCountChange(m_activeCount);
		UIManager.Instance.timerCount = timerCount;
        TimersManager.SetLoopableTimer(this, 1f, UIManager.Instance.timeCount);
		TimersManager.SetPaused(UIManager.Instance.timeCount, false);	
	}

	// Update is called once per frame
	void Update () {
		
	}
}
