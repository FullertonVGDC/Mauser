using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guiFader : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		mGuiFaderImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Color curColor = mGuiFaderImage.color;
		
		if(mIsFadingOut)
		{
			if(curColor.a < 0.0f)
			{
				curColor.a = 0.0f;
				mIsFadingOut = false;
			}
			else
			{
				mGuiFaderImage.color = new Color(
					curColor.r, 
					curColor.g, 
					curColor.b, 
					curColor.a -= 1.0f * Time.deltaTime);
			}
		}
		
		if(mIsFadingIn)
		{
			if(curColor.a > 1.0f)
			{
				curColor.a = 1.0f;
				mIsDoneFading = true;
			}
			else
			{
				mGuiFaderImage.color = new Color(
					curColor.r, 
					curColor.g, 
					curColor.b, 
					curColor.a += 1.0f * Time.deltaTime);
			}
		}
	}
	
	//Setters:
	public void SetIsFadingIn(bool sIsFadingIn)
	{
		mIsFadingIn = sIsFadingIn;
	}
	
	//Getters:
	public bool GetIsDoneFading()
	{
		return mIsDoneFading;
	}
	
	//Variables:
	
	//Checks if currently fading out.
	private bool mIsFadingOut = true;
	
	//Checks if currently fading in.
	private bool mIsFadingIn = false;
	
	//Checks if the fader is done with its fading purposes.
	private bool mIsDoneFading = false;
	
	//A quick reference to the gui fader image.
	private Image mGuiFaderImage;
}
