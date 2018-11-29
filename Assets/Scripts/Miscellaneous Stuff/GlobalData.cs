using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameMapName {GAMEMAP_BASEMENT = 0, GAMEMAP_WALLS = 1, GAMEMAP_KITCHEN = 2};

public class GlobalData : MonoBehaviour 
{
	void Awake ()
	{
		//Allow this object to always exist.
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () 
	{
        instance = this;

		//The current bound being created.
		Bounds boundBasement = new Bounds ();
		Bounds boundWalls = new Bounds ();
		Bounds boundKitchen = new Bounds ();

		mGameMapBounds = new Bounds[3];

		boundBasement.SetMinMax (new Vector3 (-1.0f, 5.0f, -10.0f), new Vector3 (161.0f, 13.0f, 10.0f));
		boundWalls.SetMinMax (new Vector3 (2.0f, 5.0f, -10.0f), new Vector3 (20.0f, 30.0f, 10.0f));
		boundKitchen.SetMinMax (new Vector3 (7.5f, 3f, -10.0f), new Vector3 (14.0f, 3f, 0.0f));

		mGameMapBounds [(int)GameMapName.GAMEMAP_BASEMENT] = boundBasement;
		mGameMapBounds [(int)GameMapName.GAMEMAP_WALLS] = boundWalls;
		mGameMapBounds [(int)GameMapName.GAMEMAP_KITCHEN] = boundKitchen;
		
		mMusicPlayer = GetComponent<AudioSource>();
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
			GameObject canvasObj = GameObject.Find("GameplayGUI");
			
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

			CameraHandler cameraSript = mMainCamera.GetComponent<CameraHandler> ();

			//Destroy the bees if they exist.
			if(mBeesGenerated == true)
			{
				foreach (Transform beeTransform in mMainCamera.transform)
				{
					Destroy(beeTransform.gameObject);
				}
				
				mBeesGenerated = false;
			}
			
			if (cameraSript != null) 
			{
				//Check if camera scrolling should be used.
				if (mCurGameMapName == GameMapName.GAMEMAP_WALLS) 
				{
					//The camera component of the main camera.
					Camera cameraComp = mMainCamera.GetComponent<Camera> ();

					if (mCheckpointEnabled)
					{
						mMainCamera.transform.position = new Vector3(0.0f, 25.0f, -1.0f);
					}
					else
					{
						mMainCamera.transform.position = new Vector3(0.0f, 4.0f, -1.0f);
					}

					cameraSript.SetIsScrollingUp (true);

					if(mBeesGenerated == false)
					{
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
                                newBee.GetComponent<SpriteRenderer>().sortingOrder = i;

								beeSpawnPos.x += spacingBetweenBees;
								degCounter += 10;
							}

							beeSpawnPos.x = bottomLeftCameraPoint.x;
							beeSpawnPos.y += spacingBetweenRows;
						}
						
						mBeesGenerated = true;
					}
					
					mMusicPlayer.clip = mWallSong;
				} 
				else if (mCurGameMapName == GameMapName.GAMEMAP_BASEMENT) 
				{
					mMainCamera.transform.position = new Vector3(0.0f, 4.0f, -1.0f);
					cameraSript.SetIsScrollingUp (false);
					mMusicPlayer.clip = mBasementSong;
				}
				else if (mCurGameMapName == GameMapName.GAMEMAP_KITCHEN)
				{
					cameraSript.SetIsScrollingUp (false);
					mMusicPlayer.clip = mKitchenSong;
				}

				cameraSript.UpdateCameraBounds (mCurGameMapBounds);
			}

			//Play the currently selected background music.
			mMusicPlayer.Play(0);
			
			mChangedGameMap = false;
		}

		//Check if a checkpoint needs to be applied to the player.
		if (mCheckpointEnabled)
		{
			GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");

			playerGameObject.transform.position = new Vector3(
				mCheckpointPosition.x, mCheckpointPosition.y, mCheckpointPosition.z);

			Destroy(GameObject.Find("checkpoint"));
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
		else if(sceneName == "level_kitchen")
		{
			mCurGameMapName = GameMapName.GAMEMAP_KITCHEN;
		}

		mChangedGameMap = true;
	}

	public void Pause()
	{
		enabled = false;
	}
	
	public void UnPause()
	{
		enabled = true;
	}
	
    //Setters.
	public void SetCheckpointEnabled(bool enabled)
	{
		mCheckpointEnabled = enabled;
	}

    public void SetCurrency(uint sCurrency)
    {
        mCurrency = sCurrency;
    }
	
	public void SetSavedCurrency(uint sSavedCurrency)
	{
		mSavedCurrency = sSavedCurrency;
	}

	public void SetCheckpointPosition(Vector3 position)
	{
		mCheckpointPosition = position;
	}

	//Getters.
    public uint GetCurrency()
    {
        return mCurrency;
    }
	
	public uint GetSavedCurrency()
	{
		return mSavedCurrency;
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

    //Singleton instance of GlobalData (this can be called from ANYWHERE)
    public static GlobalData instance;

    //Checks if currently changing the game map.
    private bool mChangedGameMap = false;

	//Checks if a checkpoint is enabled for the current game map.
	private bool mCheckpointEnabled = false;

	//Checks if the bees have already been generated.
	private bool mBeesGenerated = false;
	
    //The currency of the player. Stored here for persistency.
    private uint mCurrency = 0;
	
	//The saved currency from the previous checkpoint.
	private uint mSavedCurrency = 0;

	//The current check point position.
	private Vector3 mCheckpointPosition;

	//The name of the current game map being played.
	private GameMapName mCurGameMapName;

	//The current game map bounds for the camera.
	private Bounds mCurGameMapBounds;

	//A quick reference to the main camera.
	private GameObject mMainCamera;
	
	//A quick reference to the fader object.
	private GameObject mGuiFader;
	
	//The music player for playing the background music during gameplay.
	private AudioSource mMusicPlayer;

	//The list of all game map bounds for the camera.
	private Bounds[] mGameMapBounds;
	
	//Public variables:
	
	//The basement song.
	public AudioClip mBasementSong;
	
	//The wall song.
	public AudioClip mWallSong;
	
	//The kitchen song.
	public AudioClip mKitchenSong;
	
	//The main camera prefab.
	public GameObject mMainCameraPrefab;
	
	//The gui fader prefab.
	public GameObject mGuiFaderPrefab;

	//The bee prefab for generating the bees.
	public GameObject mBeePrefab;
}
