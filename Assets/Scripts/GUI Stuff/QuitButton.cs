using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		Button button4 = GetComponent<Button> ();

		button4.onClick.AddListener(clickEventListener);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	private void clickEventListener()
	{
		Application.Quit();
	}
}
