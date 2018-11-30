using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used for having a list of shake mods.
public class ShakeMod
{
	public float mPower;
	public float mDuration;
	public float mTimeElapsed = 0.0f;
}

public class CameraHandler : MonoBehaviour 
{
	void Start () 
	{
		mCamera = GetComponent<Camera> ();
	}
	
	void Update () 
	{
		if(mIsShaking)
		{
			ShakeAnimation();
		}
		
        screenTopEdge = mCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, -(transform.position.z))).y;
        screenBottomEdge = mCamera.ScreenToWorldPoint(new Vector3(0, 0, -(transform.position.z))).y;
        screenLeftEdge = mCamera.ScreenToWorldPoint(new Vector3(0, 0, -(transform.position.z))).x;
        screenRightEdge = mCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, -(transform.position.z))).x;
    }
	
	private void ShakeAnimation()
	{
		//The power of the current shake mod.
		float power;
		
		//The duration of the current shake mod.
		float duration;
		
		//The total power being accumulated.
		float totalPower = 0.0f;
		
		for(int i = 0; i < mShakeModList.Count;)
		{
			ShakeMod curShakeMod = mShakeModList[i];
			
			power = curShakeMod.mPower;
			duration = curShakeMod.mDuration;
			
			if(curShakeMod.mTimeElapsed < duration)
			{	
				curShakeMod.mTimeElapsed += Time.deltaTime;
				totalPower += power;
				
				i++;
			}
			else
			{
				mShakeModList.RemoveAt(i);
			}
		}
		
		if(mShakeModList.Count == 0)
		{
			mIsShaking = false;
		}
		
		transform.position = new Vector3(
			transform.position.x + Random.Range(-totalPower, totalPower),
			transform.position.y + Random.Range(-totalPower, totalPower),
			transform.position.z + Random.Range(-totalPower, totalPower));
	}

	public void UpdateCameraBounds(Bounds bounds)
	{
		mCameraBounds = bounds;
	}
	
	public void StartShake(float power, float duration)
	{
		mIsShaking = true;
		
		//The current shake mod being created.
		ShakeMod shakeMod = new ShakeMod();
		
		shakeMod.mPower = power;
		shakeMod.mDuration = duration;
		
		mShakeModList.Add(shakeMod);
	}

	public Bounds GetBounds()
	{
		return mCameraBounds;
	}

    //Variables:

    //Screen edge tracking
    [HideInInspector]
    public float screenTopEdge;
    [HideInInspector]
    public float screenBottomEdge;
    [HideInInspector]
    public float screenLeftEdge;
    [HideInInspector]
    public float screenRightEdge;
	
	//Checks if the camera is shaking.
	private bool mIsShaking = false;

	//The srict aspect ratio for the camera.
	private float mAspectRatio;

	//The width of the camera view.
	private float mCameraOrthoWidth;
	
	//The height of the camera view.
	public float mCameraOrthoHeight;
	
	//The list of shake modifiers.
	private List<ShakeMod> mShakeModList = new List<ShakeMod>();

	//The current camera bounds. Set by the current game map.
	private Bounds mCameraBounds;

	//The camera component of this camera object.
	private Camera mCamera;
}
