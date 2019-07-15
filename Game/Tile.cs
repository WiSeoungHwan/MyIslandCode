﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileState{
	normal,
	material,
	building
}
public class Tile: MonoBehaviour{
	[SerializeField]
	public TileState state = TileState.normal;

	public int index;
	void Start(){
		setTilePrefab();
	}

	private void setTilePrefab(){
		switch (state){
			case TileState.normal:
				loadPrefabs("Normal");
				break;
			case TileState.material:
				loadPrefabs("Material");
				break;
			case TileState.building:
				loadPrefabs("Building");
				break;
		}
	}

	private void loadPrefabs(string forderName){
		if (Resources.LoadAll("Prefab/Tile/"+forderName) == null){Debug.Log("Err: "+forderName+"is null");return;}
		Object[] prefabArr = Resources.LoadAll("Prefab/Tile/"+forderName);
		if (prefabArr.Length == 0) {Debug.Log(forderName + " has " + prefabArr.Length + " prefab");return;}
		GameObject prefabObject = Instantiate(prefabArr[Random.Range(0,prefabArr.Length)],gameObject.transform.position,Quaternion.identity) as GameObject;
		prefabObject.transform.parent = gameObject.transform;
		gameObject.tag = forderName;
		if (forderName == "Normal"){
			gameObject.AddComponent<BoxCollider>();
		}else{
			CapsuleCollider capsulecolider = gameObject.AddComponent<CapsuleCollider>();
			capsulecolider.height = 2;
			capsulecolider.center = new Vector3(0,0.5f,0);
		}
		Debug.Log(prefabObject.tag);
		prefabObject.SetActive(true);
	}
}


