using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level2Button : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Button button2 = GetComponent<Button> ();
        
		button2.onClick.AddListener(clickEventListener);

		mGlobalData = GameObject.Find ("globalData").GetComponent<globalData> ();
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

	private globalData mGlobalData;
}
