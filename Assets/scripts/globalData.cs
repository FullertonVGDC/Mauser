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

		boundBasement.SetMinMax (new Vector3 (-1.0f, 5.0f, -10.0f), new Vector3 (161.0f, 13.0f, 10.0f));
		boundWalls.SetMinMax (new Vector3 (4.0f, 5.0f, -10.0f), new Vector3 (16.0f, 30.0f, 10.0f));
		boundKitchen.SetMinMax (new Vector3 (8.0f, 5.0f, -10.0f), new Vector3 (30.0f, 8.0f, 10.0f));

		mGameMapBounds [(int)GameMapName.GAMEMAP_BASEMENT] = boundBasement;
		mGameMapBounds [(int)GameMapName.GAMEMAP_WALLS] = boundWalls;
		mGameMapBounds [(int)GameMapName.GAMEMAP_KITCHEN] = boundKitchen;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	//Scene loading event management.
	
	//Add the event listener for loading the next scene.
	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	
	//Remove the event listener for loading the next scene.
	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
	
	//Update for when the scene changes.
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if(mChangedGameMap) 
        {
			//A reference to the canvas object.
			GameObject canvasObj = GameObject.Find("Canvas");
			
			//Create the main camera if we haven't already.
			if(mMainCamera == null)
			{
				mMainCamera = Instantiate(mMainCameraPrefab);
			}
			
			//Create the fader object.
			if(canvasObj != null)
			{
				mGuiFader = Instantiate(mGuiFaderPrefab);
				mGuiFader.transform.SetParent(canvasObj.transform, false);
			}
			else
			{
				Debug.Log("Error: Canvas object could not be found.");
			}

			mCurGameMapBounds = mGameMapBounds [(int)mCurGameMapName];

			camera cameraSript = mMainCamera.GetComponent<camera> ();

			if (cameraSript != null) 
			{
				//Check if camera scrolling should be used.
				if (mCurGameMapName == GameMapName.GAMEMAP_WALLS) 
				{
					//The camera component of the main camera.
					Camera cameraComp = mMainCamera.GetComponent<Camera> ();

					mMainCamera.transform.position = new Vector3 (0.0f, 4.0f, -1.0f);
					cameraSript.SetIsScrollingUp (true);

					float numberOfRows = 4;
					float spacingBetweenRows = 0.35f;
					float spacingBetweenBees = 1.0f;

					Vector2 bottomLeftCameraPoint = cameraComp.ScreenToWorldPoint (new Vector2 (0, 0));
					Vector2 bottomRightCameraPoint = cameraComp.ScreenToWorldPoint (new Vector2 (cameraComp.pixelWidth, 0));
					Vector2 beeSpawnPos = bottomLeftCameraPoint;

					float degCounter = 0;

					for (int i = 0; i < numberOfRows; i++) 
					{
						while (beeSpawnPos.x < bottomRightCameraPoint.x) 
						{
							GameObject newBee = Instantiate (mBeePrefab, beeSpawnPos, Quaternion.identity);
							newBee.transform.parent = mMainCamera.transform;
							newBee.GetComponent<Bee> ().deg = degCounter;
							newBee.GetComponent<Bee> ().anchorPos = beeSpawnPos;

							beeSpawnPos.x += spacingBetweenBees;
							degCounter += 10;
						}

						beeSpawnPos.x = bottomLeftCameraPoint.x;
						beeSpawnPos.y += spacingBetweenRows;
					}
				} 
				else if (mCurGameMapName == GameMapName.GAMEMAP_BASEMENT) 
				{
					mMainCamera.transform.position = new Vector3(0.0f, 4.0f, -1.0f);
					cameraSript.SetIsScrollingUp (false);
				}
				else
				{
					cameraSript.SetIsScrollingUp (false);
				}

				cameraSript.UpdateCameraBounds (mCurGameMapBounds);
			}

			mChangedGameMap = false;
		}
	}
	
	public void ChangeMap(string sceneName)
	{
		SceneManager.LoadScene (sceneName);

		//Choose the correct game map name for the camera boundaries.
		if(sceneName == "level_wall_fade")
		{
			mCurGameMapName = GameMapName.GAMEMAP_WALLS;
		}
		else if(sceneName == "level_wall")
		{
			mCurGameMapName = GameMapName.GAMEMAP_WALLS;
		}

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
	
	//A quick reference to the fader object.
	private GameObject mGuiFader;

	//The list of all game map bounds for the camera.
	private Bounds[] mGameMapBounds;
	
	//Public variables:
	
	//The main camera prefab.
	public GameObject mMainCameraPrefab;
	
	//The gui fader prefab.
	public GameObject mGuiFaderPrefab;

	//The bee prefab for generating the bees.
	public GameObject mBeePrefab;
}
