using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuResumeButton : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		Button button = GetComponent<Button> ();

		button.onClick.AddListener(clickEventListener);

		mGameplayGUI = GameObject.Find ("GameplayGUI").GetComponent<GameplayGUI> ();

		mMusicPlayer = GameObject.Find ("GlobalData").GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	private void clickEventListener()
	{
		mGameplayGUI.SetPaused(false);
		mMusicPlayer.volume = 0.8f;
	}

	//The gui for the gameplay GUI.
	private GameplayGUI mGameplayGUI;

	//The music player for the game.
	private AudioSource mMusicPlayer;
}
