using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timers;


public class GameManager : SingletonMonoBehaviour<GameManager> {
	[SerializeField]
	Ground myGround;
	public int turn = 1;
	public bool gameTrigger = false;
	public int timerCount;
	public int activeCount;
	
	int m_activeCount;
	// Use this for initialization
	protected override void OnAwake(){
		
		m_activeCount = activeCount;
	}
	protected override void OnStart(){
		UIManager.Instance.timerCount = timerCount;
	}
	public void timerSetting(){
		gameTrigger = true;
		foreach( var towers in  TowerManager.Instance.towers){
			towers.shootOnce();
			towers.target = TowerManager.Instance.enemyGround.tileArr[Random.Range(0,24)].transform;
		}
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
