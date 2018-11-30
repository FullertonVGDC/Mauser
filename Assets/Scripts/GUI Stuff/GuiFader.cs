using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiFader : MonoBehaviour 
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
			if(mGraceFadeOutPeriodAmount < mGraceFadeOutPeriod)
			{
				mGraceFadeOutPeriodAmount += Time.deltaTime;
			}
			else
			{
				if(curColor.a > 1.0f)
				{
					curColor.a = 1.0f;
					mIsDoneFading = true;
					mGraceFadeOutPeriodAmount = 0.0f;
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
	}
	
	//Setters:
	public void SetIsFadingIn(bool sIsFadingIn)
	{
		mIsFadingIn = sIsFadingIn;
	}

	public void SetGraceFadeOutPeriodAmount(float amount)
	{
		mGraceFadeOutPeriodAmount = amount;
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
	
	//The period at which to begin fading out.
	private float mGraceFadeOutPeriod = 3.0f;
	
	//The amount at which to begin fading out.
	private float mGraceFadeOutPeriodAmount = 0.0f;
	
	//A quick reference to the gui fader image.
	private Image mGuiFaderImage;
}
