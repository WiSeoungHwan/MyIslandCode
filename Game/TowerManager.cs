using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : SingletonMonoBehaviour<TowerManager> {

	[SerializeField]
	public Ground enemyGround;

	public List<Tower> towers = new List<Tower>();

	// Use this for initialization
	
	protected override void OnStart(){
		
	}

	public void randomTowerTarget(){
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
