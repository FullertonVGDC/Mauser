using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level3Button : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Button button3 = GetComponent<Button> ();

		button3.onClick.AddListener(clickEventListener);

		mGlobalData = GameObject.Find ("globalData").GetComponent<globalData> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	private void clickEventListener()
	{
		Debug.Log("Clicked!");

		string sceneName = "level_kitchen";

		Destroy(GameObject.Find("MainMenuCamera"));
		mGlobalData.ChangeMap (sceneName);

	}

	private globalData mGlobalData;
}
