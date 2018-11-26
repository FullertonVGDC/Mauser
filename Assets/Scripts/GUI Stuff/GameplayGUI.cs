using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayGUI : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		mPlayerComp = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		mGlobalDataComp = GameObject.Find("globalData").GetComponent<GlobalData>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//The player's health.
		uint playerHealth = mPlayerComp.GetHealth();
		
		//The player's armor cookies.
		uint playerArmorCookies = mPlayerComp.GetArmorCookies();
		
		//The number of bottle caps collected.
		int playerBottleCaps = mGlobalDataComp.GetCurrency();
		
		//Create the cookies.
		if(playerHealth != mCurPlayerHealth)
		{
			mCurPlayerHealth = playerHealth;
			
			//Destroy the old cookies.
			foreach (Transform childTransform in transform)
			{
				if(childTransform.gameObject.tag == "cookieUI")
				{
					Destroy(childTransform.gameObject);
				}
			}
			
			//Create the new cookies.
			for (uint i = 0; i < mCurPlayerHealth; i++)
			{
				GameObject curCookieUI;
				
				curCookieUI = Instantiate(mCookieUIPrefab, new Vector3(
					200.0f + (i * 70.0f), 560.0f, -10.0f), 
					Quaternion.identity);
					
				curCookieUI.transform.SetParent(transform.GetChild(1).transform);
			}
		}
		
		//Create the armor cookies.
		if(playerArmorCookies != mCurPlayerArmorCookies)
		{
			mCurPlayerArmorCookies = playerArmorCookies;
			
			//Destroy the old armor cookies.
			foreach (Transform childTransform in transform)
			{
				if(childTransform.gameObject.tag == "armorCookieUI")
				{
					Destroy(childTransform.gameObject);
				}
			}
			
			//Create the new armor cookies.
			for (uint i = 0; i < mCurPlayerArmorCookies; i++)
			{
				GameObject curArmorCookieUI;
				
				curArmorCookieUI = Instantiate(mArmorCookieUIPrefab, new Vector2(
					200.0f + ((i+3) * 70.0f), 560.0f), 
					Quaternion.identity);
					
				curArmorCookieUI.transform.SetParent(transform.GetChild(1).transform);
			}
		}
		
		//Update the bottle cap values.
		if(playerBottleCaps != mCurPlayerBottleCaps)
		{
			mCurPlayerBottleCaps = playerBottleCaps;
			
			//The text component of the bottle caps UI.
			Text textComp = transform.GetChild(0).GetChild(2).GetComponent<Text>();
			
			textComp.text = mCurPlayerBottleCaps.ToString();
		}
	}
	
	//The current player health cached by the player.
	private uint mCurPlayerHealth;
	
	//The current player armor cookies cached by the player.
	private uint mCurPlayerArmorCookies;
	
	//The current player bottle caps cached by the player.
	private int mCurPlayerBottleCaps;
	
	//The player component.
	private Player mPlayerComp;
	
	//The global data component.
	private GlobalData mGlobalDataComp;
	
	//Prefabs.
	
	//The prefab for the cookie UI.
	public GameObject mCookieUIPrefab;
	
	//The prefab for the armor cookie UI.
	public GameObject mArmorCookieUIPrefab;
}
