using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour {
	[SerializeField]
	GameObject buildPanel;
	[SerializeField]
	GameObject buildOpen;
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
	}

	public void homeButtonClick(){
		Player.Instance.isBuildingMode = true;
		Player.Instance.playerDel();
	}
}
