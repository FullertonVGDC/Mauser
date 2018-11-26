using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2Button : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		Button button2 = GetComponent<Button> ();
        
		button2.onClick.AddListener(clickEventListener);

		mGlobalData = GameObject.Find ("GlobalData").GetComponent<GlobalData> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	private void clickEventListener()
	{
		//Debug.Log("Clicked!");

		string sceneName = "level_wall_fade";

		Destroy(GameObject.Find("MainMenuCamera"));
		mGlobalData.ChangeMap (sceneName);

	}

	private GlobalData mGlobalData;
}
