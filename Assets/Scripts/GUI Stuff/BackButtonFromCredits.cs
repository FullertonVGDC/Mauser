using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButtonFromCredits : MonoBehaviour 
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
		SceneManager.LoadScene ("main_menu");
	}
}
