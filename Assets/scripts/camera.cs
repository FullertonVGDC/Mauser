using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		mCamera = GetComponent<Camera> ();

		mAspectRatio = 1.777777777777778f;

		mCamera.rect = new Rect (0.0f, 0.0f, mCameraOrthoHeight * mAspectRatio, mCameraOrthoHeight);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void UpdateCameraBounds(Bounds bounds)
	{
		mCameraBounds = bounds;
	}

	//Getters:
	public Bounds GetBounds()
	{
		return mCameraBounds;
	}

	//Varaibles:

	//The srict aspect ratio for the camera.
	private float mAspectRatio;

	//The width of the camera view.
	private float mCameraOrthoWidth;

	//The height of the camera view.
	public float mCameraOrthoHeight;

	//The current camera bounds. Set by the current game map.
	private Bounds mCameraBounds;

	//The camera component of this camera object.
	private Camera mCamera;
}
