using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameMapName {GAMEMAP_BASEMENT = 0, GAMEMAP_WALLS = 1, GAMEMAP_KITCHEN = 2};

public class globalData : MonoBehaviour 
{

	void Awake ()
	{
		//Allow this object to always exist.
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () 
	{
		//The current bound being created.
		Bounds boundBasement = new Bounds ();
		Bounds boundWalls = new Bounds ();
		Bounds boundKitchen = new Bounds ();

		mGameMapBounds = new Bounds[3];

		boundBasement.SetMinMax (new Vector3 (8.0f, 5.0f, -10.0f), new Vector3 (30.0f, 8.0f, 10.0f));
		boundWalls.SetMinMax (new Vector3 (4.0f, 5.0f, -10.0f), new Vector3 (16.0f, 30.0f, 10.0f));
		boundKitchen.SetMinMax (new Vector3 (8.0f, 5.0f, -10.0f), new Vector3 (30.0f, 8.0f, 10.0f));

		mGameMapBounds [(int)GameMapName.GAMEMAP_BASEMENT] = boundBasement;
		mGameMapBounds [(int)GameMapName.GAMEMAP_WALLS] = boundWalls;
		mGameMapBounds [(int)GameMapName.GAMEMAP_KITCHEN] = boundKitchen;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(mChangedGameMap) 
        {
			mMainCamera = GameObject.Find ("MainCamera");
            Debug.Log("Changed to main camera.");

			if(mMainCamera != null) 
            {
				mCurGameMapBounds = mGameMapBounds [(int)mCurGameMapName];

				camera cameraSript = mMainCamera.GetComponent<camera> ();

				if (cameraSript != null) 
				{
					//Check if camera scrolling should be used.
					if (mCurGameMapName == GameMapName.GAMEMAP_WALLS) 
					{
						cameraSript.SetIsScrollingUp (true);
					} 
					else 
					{
						cameraSript.SetIsScrollingUp (false);
					}

					Debug.Log ("Changed camera bounds.");
					cameraSript.UpdateCameraBounds (mCurGameMapBounds);
				}
			}

			mChangedGameMap = false;
		}
	}

	public void ChangeMap(string sceneName, GameMapName gameMapName)
	{
		SceneManager.LoadScene (sceneName);

		mCurGameMapName = gameMapName;

		mChangedGameMap = true;
	}

    //Setters.
    public void SetCurrency(int sCurrency)
    {
        mCurrency = sCurrency;
    }

	//Getters.
    public int GetCurrency()
    {
        return mCurrency;
    }

	public GameMapName GetCurrentGameMapName()
	{
		return mCurGameMapName;
	}

	public Bounds GetCurrentBound()
	{
		return mCurGameMapBounds;
	}

    //Variables.

	//Checks if currently changing the game map.
	private bool mChangedGameMap = false;

    //The currency of the player. Stored here for persistency.
    private int mCurrency = 0;

	//The name of the current game map being played.
	private GameMapName mCurGameMapName;

	//The current game map bounds for the camera.
	private Bounds mCurGameMapBounds;

	//A quick reference to the main camera.
	private GameObject mMainCamera;

	//The list of all game map bounds for the camera.
	private Bounds[] mGameMapBounds;
}
