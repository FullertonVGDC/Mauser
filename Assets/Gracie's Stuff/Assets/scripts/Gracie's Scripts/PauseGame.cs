using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseGame : MonoBehaviour {

	public Transform canvas;
	public Button resumeButton;
	public Button levelSelectButton;
	public Button quitButton;
	public string sceneName;


	public void openGame() {
		SceneManager.LoadScene ("scene2");
		// enter the scene name 

	}

	public void resumeGame() {
		canvas.gameObject.SetActive (false);
		Time.timeScale = 1;
	}

	public void ExitGame() {
		Application.Quit ();
	}

	public void Pause() {
		if (canvas.gameObject.activeInHierarchy == false) {
			canvas.gameObject.SetActive (true);
			Time.timeScale = 0;
		} else {
			canvas.gameObject.SetActive (false);
			Time.timeScale = 1;
		}
	}
		
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Pause ();
		}
		resumeButton.onClick.AddListener (resumeGame);
		levelSelectButton.onClick.AddListener (openGame);
		quitButton.onClick.AddListener (ExitGame);
	}

}
/*
 * 
}

 * */
