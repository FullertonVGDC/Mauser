using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class return_button : MonoBehaviour {

	public Button returnButton;

	public void exitMinigame() {
		SceneManager.LoadScene ("minigame_scene");
		// enter the scene name for the main game

	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		returnButton.onClick.AddListener (exitMinigame);
	}
}
