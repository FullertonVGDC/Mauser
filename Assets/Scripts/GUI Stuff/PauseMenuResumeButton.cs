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
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	private void clickEventListener()
	{
		mGameplayGUI.SetPaused(false);
	}

	private GameplayGUI mGameplayGUI;
}
