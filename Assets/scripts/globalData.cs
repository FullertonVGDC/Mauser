using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameMapName {GAMEMAP_BASEMENT = 0, GAMEMAP_WALLS = 1, GAMEMAP_KITCHEN = 2};

public class globalData : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		mChangedGameMap = false;

		mMainCamera = GameObject.Find ("Main Camera");

		mGameMapBounds = new Bounds[3];

		//The current bound being created.
		Bounds bound1 = new Bounds();

		bound1.SetMinMax(new Vector3(8.0f, 5.0f, -10.0f), new Vector3(30.0f, 8.0f, 10.0f));

		mGameMapBounds [(int)GameMapName.GAMEMAP_BASEMENT] = bound1;
		mGameMapBounds [(int)GameMapName.GAMEMAP_WALLS] = bound1;
		mGameMapBounds [(int)GameMapName.GAMEMAP_KITCHEN] = bound1;

		//TODO: For now, set the game map name and bounds here.
		mCurGameMapName = GameMapName.GAMEMAP_BASEMENT;
		mCurGameMapBounds = mGameMapBounds [(int)GameMapName.GAMEMAP_BASEMENT];

		//TODO: For now, set the changing game map to true.
		mChangedGameMap = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (mChangedGameMap == true) 
		{
			camera cameraSript = mMainCamera.GetComponent<camera> ();
				
			if (cameraSript != null) 
			{
				cameraSript.UpdateCameraBounds (mCurGameMapBounds);
			}

			mChangedGameMap = false;
		}
	}

	//Getters.
	GameMapName GetCurrentGameMapName()
	{
		return mCurGameMapName;
	}

	Bounds GetCurrentBound()
	{
		return mCurGameMapBounds;
	}

	//Checks if currently changing the game map.
	bool mChangedGameMap;

	//The name of the current game map being played.
	private GameMapName mCurGameMapName;

	//The current game map bounds for the camera.
	private Bounds mCurGameMapBounds;

	//A quick reference to the main camera.
	private GameObject mMainCamera;

	//The list of all game map bounds for the camera.
	private Bounds[] mGameMapBounds;
}
