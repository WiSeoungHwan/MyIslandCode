﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {
	// - Properties
	public List<Tile> tileArr = new List<Tile>();
	// Use this for initialization
	void Awake() {
		setTransFormArr();
	}
	void Start(){
		// setTransFormArr();
	}

	void setTransFormArr(){
		var index = 0;
		for(int i = 0; i < 5; i++){
			for(int j = 0; j < 5; j++){
				if (Resources.Load("Prefab/Tile/tile") == null) {Debug.Log("Err: tile is null");return;}
				Vector3 pos = new Vector3(i + transform.position.x,gameObject.transform.position.y,j);
				GameObject tileObject = Instantiate(Resources.Load("Prefab/Tile/tile"),pos,Quaternion.identity) as GameObject;
				var tile = tileObject.GetComponent<Tile>();
				tile.index = index;
				if (i == 0 && j == 0){
					tile.state = TileState.normal;
				}else {
					// .normal, .material 
					tile.state = (TileState)Random.Range(0,2);
					// test
					// tile.state = (TileState)0;
				}
				tileObject.transform.parent = gameObject.transform;
				tileObject.SetActive(true);
				tileArr.Add(tile);
				index++;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
