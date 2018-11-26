﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level3Button : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		Button button3 = GetComponent<Button> ();

		button3.onClick.AddListener(clickEventListener);

		mGlobalData = GameObject.Find ("GlobalData").GetComponent<GlobalData> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	private void clickEventListener()
	{
		//Debug.Log("Clicked!");

		string sceneName = "level_kitchen";

		Destroy(GameObject.Find("MainMenuCamera"));
		mGlobalData.ChangeMap (sceneName);

	}

	private GlobalData mGlobalData;
}
