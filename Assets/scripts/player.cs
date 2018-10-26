using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		mTransform = GetComponent<Transform> ();
		mRigidBody2D = GetComponent<Rigidbody2D> ();

		collidableLayerMask = LayerMask.GetMask ("collidable");

		//Get the main camera.
		mCameraObject = GameObject.Find("MainCamera");
		mCameraScript = mCameraObject.GetComponent<camera>();
		mCameraTransform = mCameraObject.transform;

		mSpriteRenderer = GetComponent<SpriteRenderer>();

        //Get the global data.
        mGlobalData = GameObject.Find("globalData").GetComponent<globalData>();

	mFiringKey = KeyCode.F;
	}

	// Update is called once per frame
	void Update () 
	{
		Movement ();
	        BulletCreation();
	        UpdateCameraPosition();
		CheckIfBelowCamera ();

		//Compute invincibility period.
		if(mIsHurt == true)
		{
			if(mHurtInvincibilityPeriodAmount > 0.1)
			{
				mSpriteRenderer.enabled = true;
			}

			mHurtInvincibilityPeriodAmount += Time.deltaTime;

			if(mHurtInvincibilityPeriodAmount >= mHurtInvincibilityPeriod)
			{
				mIsHurt = false;
			}
		}
	}

	//Use this for anything involving collision or anything else that is discrete every 1 / 60th of a frame.
	void FixedUpdate()
	{
		//Check if grounded.
		if(mRigidBody2D.velocity.y <= 0.0f)
		{
			//Perform a raycast below the player to check if the player is grounded.

			//The Left ray.
			RaycastHit2D groundRayL = Physics2D.Raycast(mTransform.position - new Vector3(0.5f, 0.5f, 0.0f), 
				new Vector3(0.0f, -1.0f, 0.0f), 0.2f, collidableLayerMask);

			//The right ray.
			RaycastHit2D groundRayR = Physics2D.Raycast(mTransform.position - new Vector3(-0.5f, 0.5f, 0.0f), 
				new Vector3(0.0f, -1.0f, 0.0f), 0.2f, collidableLayerMask);

            //The middle ray.
            RaycastHit2D groundRayM = Physics2D.Raycast(mTransform.position - new Vector3(0.0f, 0.5f, 0.0f), 
                new Vector3(0.0f, -1.0f, 0.0f), 0.2f, collidableLayerMask);

			//If the collision occurs, check if grounded. If not, assume the player is in the air and set grounded to 
			// false.
			if (groundRayL) 
			{
				//If the distance is smaller than the previous distance, wait until the object stops falling. If the 
				// distance is larger than or equal to the previous distance, then the object is grounded.
				if (groundRayL.distance < mPrevDistanceFromGroundL) 
				{
					mPrevDistanceFromGroundL = groundRayL.distance;
				} 
				else 
				{
					mPrevDistanceFromGroundL = 999.0f;
					mGrounded = true;
				}
			} 
            else if (groundRayR) 
            {
                //If the distance is smaller than the previous distance, wait until the object stops falling. If the 
                // distance is larger than or equal to the previous distance, then the object is grounded.
                if (groundRayL.distance < mPrevDistanceFromGroundR) 
                {
                    mPrevDistanceFromGroundR = groundRayL.distance;
                } 
                else 
                {
                    mPrevDistanceFromGroundR = 999.0f;
                    mGrounded = true;
                }
            } 
            else if (groundRayM) 
            {
                //If the distance is smaller than the previous distance, wait until the object stops falling. If the 
                // distance is larger than or equal to the previous distance, then the object is grounded.
                if (groundRayM.distance < mPrevDistanceFromGroundM) 
                {
                    mPrevDistanceFromGroundM = groundRayM.distance;
                } 
                else 
                {
                    mPrevDistanceFromGroundM = 999.0f;
                    mGrounded = true;
                }
            } 
			else 
			{
				mGrounded = false;
			}
		}
	}

	//Collision Callbacks (Non-Trigger).
	void OnCollisionEnter2D(Collision2D collision)
	{
		
	}

	//Collision Callbacks on Enter(Trigger).
	void OnTriggerEnter2D(Collider2D collider)
	{
		//Check if colliding with golden cheese.
		if (collider.gameObject.tag == "gold") 
		{
            //The bottle cap object being collided with.
            bottleCap bottleCap1 = collider.gameObject.GetComponent<bottleCap>();

            mGlobalData.SetCurrency(mGlobalData.GetCurrency() + bottleCap1.GetCurrency());

            Debug.Log("Currency: " + mGlobalData.GetCurrency());

			Destroy(collider.gameObject);
		}
	}

	//Collision Callbacks on Stay(Trigger).
	void OnTriggerStay2D(Collider2D collider)
	{
		//Check if colliding with an enemy.
		if (collider.gameObject.tag == "enemy1") 
		{
            //The other enemy that the player is colliding with.
            enemy enemy1 = collider.gameObject.GetComponent<enemy>();

            //Only get affected by the enemy if it isn't already dead.
            if (!enemy1.GetIsDead())
            {
                //If the player isn't currently hurt, hurt the player.
                if (mIsHurt == false && mIsDead == false)
                {
                    mIsBeingKnockedBack = true;
                    mWalkingLeft = false;
                    mWalkingRight = false;
                    mGrounded = false;
                    mIsHurt = true;

                    mSpriteRenderer.enabled = false;

                    //Kill the player if out of health. Otherwise take damage.
                    if (mCurHealth == 1)
                    {
                        Debug.Log("Dead!");
                        mIsDead = true;

                        if (mRigidBody2D.velocity.x > 0)
                        {
                            mRigidBody2D.velocity = new Vector2(-5.0f, 20.0f);
                            mFacingRight = true;
                        }
                        else if (mRigidBody2D.velocity.x < 0)
                        {
                            mRigidBody2D.velocity = new Vector2(5.0f, 20.0f);
                            mFacingRight = false;
                        }
                        else
                        {
                            mRigidBody2D.velocity = new Vector2(0.0f, 20.0f);
                        }
                    }
                    else
                    {
                        mCurHealth--;
                        mHurtInvincibilityPeriodAmount = 0.0f;

                        if (mRigidBody2D.velocity.x > 0)
                        {
                            mRigidBody2D.velocity = new Vector2(-3.0f, 10.0f);
                            mFacingRight = true;
                        }
                        else if (mRigidBody2D.velocity.x < 0)
                        {
                            mRigidBody2D.velocity = new Vector2(3.0f, 10.0f);
                            mFacingRight = false;
                        }
                        else
                        {
                            mRigidBody2D.velocity = new Vector2(0.0f, 10.0f);
                        }
                    }
                }
            }
		}
	}

	//Methods.
	void Movement()
	{
		//Only allow the player to move if the player is not dead and not being knocked back.
		if(mIsBeingKnockedBack == false && mIsDead == false)
		{
			//The final walk speed.
			float finalWalkSpeed = 0.0f;

			//The final vertical speed.
			float finalVerticalSpeed = mRigidBody2D.velocity.y;

			//Checks if the jump key is pressed.
			bool jumpingAllowed = false;

			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			//Process movement for left and right walking.
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

			//--------------------------------------------------------------------------------------------------------------
			//Checking for pressed keys.
			//--------------------------------------------------------------------------------------------------------------

			//Check if the left walking keys are down.
			if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			{
	            mFacingRight = false;
				mWalkingLeft = true;
			}

			//Check if the right walking keys are down.
			if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
	            mFacingRight = true;
				mWalkingRight = true;
			}

			//Check if the jumping keys are down.
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
			{
				//Only jump if the player is grounded.
				if (mGrounded) 
				{
					mGrounded = false;
					jumpingAllowed = true;
				}
			}

	        //Check if the quit key is down.
	        if(Input.GetKeyDown(KeyCode.Escape))
	        {
	            Application.Quit();
	        }

			//--------------------------------------------------------------------------------------------------------------
			//Checking for released keys.
			//--------------------------------------------------------------------------------------------------------------

			//Check if the left walking keys are released.
			if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
			{
				mWalkingLeft = false;
			}

			//Check if the right walking keys are released.
			if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
			{
				mWalkingRight = false;
			}

			//--------------------------------------------------------------------------------------------------------------
			//Process the movement.
			//--------------------------------------------------------------------------------------------------------------

			if (!(mWalkingLeft && mWalkingRight)) 
			{
				//Set the walking speed if left walking.
				if (mWalkingLeft) 
				{
					finalWalkSpeed = -mBaseWalkSpeed;
				}

				//Set the walking speed if right walking.
				if (mWalkingRight) 
				{
					finalWalkSpeed = mBaseWalkSpeed;
				}
			}

			//If the jumping key is pressed and was validated, then set the base jump speed.
			if (jumpingAllowed) 
			{
				finalVerticalSpeed = mBaseJumpSpeed;
			}

			//Set the final velocity.
			mRigidBody2D.velocity = new Vector3(finalWalkSpeed, finalVerticalSpeed, 0.0f);
		}
		else
		{
			if(mGrounded)
			{
				mRigidBody2D.velocity = new Vector2(0.0f, mRigidBody2D.velocity.y);
			}

			if(mHurtInvincibilityPeriodAmount >= mKnockbackPeriod)
			{
				mIsBeingKnockedBack = false;
			}
		}
	}

    void BulletCreation()
    {
        //Check if the left walking keys are down.
        if(Input.GetKeyDown(mFiringKey))
        {
            if (mIsFiringBullets == false)
            {
                mIsFiringBullets = true;
                mFiringPeriodAmount = mFiringPeriod;
            }
        }

        else if(Input.GetKeyUp(mFiringKey))
        {
            mIsFiringBullets = false;
        }

        mFiringPeriodAmount += Time.deltaTime;

        //If firing bullets, create them after the correct amount of time has elapsed.
        if(mIsFiringBullets == true)
        {
            while (mFiringPeriodAmount >= mFiringPeriod)
            {
                GameObject bulletPrefab = Instantiate(mBulletPrefab);
                bulletPrefab.transform.position = new Vector3(mTransform.position.x, 
                    mTransform.position.y, mTransform.position.z);

                bullet bulletComponent = bulletPrefab.GetComponent<bullet>();

                bulletComponent.SetFacingRight(mFacingRight);

                mFiringPeriodAmount -= mFiringPeriod;
            }
        }
    }

    void UpdateCameraPosition()
    {
    	//A quick reference to the player's position.
    	Vector3 curPosition = mTransform.position;

    	//The current bounds of the camera.
    	Bounds curBounds = mCameraScript.GetBounds ();

    	//Camera offset for forcing the camera away from the player.
    	Vector3 cameraOffset = new Vector3 (0.0f, 0.0f, 0.0f);

    		float transformY;

    		bool cameraIsScrollingUp = mCameraScript.GetIsScrollingUp ();

    		if (cameraIsScrollingUp) 
    		{
    			transformY = mCameraTransform.position.y + (mScrollSpeed * Time.deltaTime);
    		} 
    		else 
    		{
    			transformY = mTransform.position.y;
    		}
    			
    	//The final camera position.
    	Vector3 finalCameraPosition = new Vector3 (mTransform.position.x, transformY, 
    	                                    mCameraTransform.position.z);

    	//Check if the camera is outside the x bounds.
    	if (curPosition.x < curBounds.min.x) 
        {
    		cameraOffset.x = curBounds.min.x - curPosition.x;
    	} 
        else if (curPosition.x > curBounds.max.x) 
        {
    		cameraOffset.x = curBounds.max.x - curPosition.x;
    	}

    	if (!cameraIsScrollingUp) 
        {
    		//Check if the camera is outside the y bounds.
    		if (curPosition.y < curBounds.min.y) 
            {
    			cameraOffset.y = curBounds.min.y - curPosition.y;
    		} 
            else if (curPosition.y > curBounds.max.y) 
            {
    			cameraOffset.y = curBounds.max.y - curPosition.y;
    		}
    	}

    	finalCameraPosition += cameraOffset;

    	mCameraTransform.position = finalCameraPosition;
    }

	void CheckIfBelowCamera()
	{
		if (!mIsDead && mTransform.position.y < (mCameraTransform.position.y - 6.0f)) 
		{
			mIsDead = true;
			Debug.Log ("Fell out of the level.");
			mWalkingLeft = false;
			mWalkingRight = false;
			mRigidBody2D.velocity = new Vector3 (0.0f, 0.0f, 0.0f);
		}
	}

	//Variables:

	//The current health the player has.
	private uint mCurHealth = 3;

	//The maximum health the player can hold.
	private uint mMaxHealth = 3;

	//The base walking speed.
	private float mBaseWalkSpeed = 6.0f;

	//The base jump speed.
	private float mBaseJumpSpeed = 16.0f;

	//The previous distance from the ground for the left ray.
	private float mPrevDistanceFromGroundL = 999.0f;

	//The previous distance from the ground for the right ray.
	private float mPrevDistanceFromGroundR = 999.0f;

    //The previous distance from the ground for the middle ray.
    private float mPrevDistanceFromGroundM = 999.0f;

    //The time elapsed for a single bullet to fire. The denominator is the bullets per second 
    // fired by the player.
    private float mFiringPeriod = 1.0f / 6.0f;

    //The amount of time elapsed for firing bullets. It is initially set to the firing frequency.
    private float mFiringPeriodAmount;

	//The current invincibility amount after the player gets hurt. When equal to max, the player
	// is no longer invincible.
	private float mHurtInvincibilityPeriodAmount = 0.0f;

	//The maximum hurt invincibility amount in seconds.
	private float mHurtInvincibilityPeriod = 2.0f;

	//The scrolling speed for the camera in the wall level.
	public float mScrollSpeed = 0.5f;

	//The maximum knockback amount in seconds.
	private float mKnockbackPeriod = 0.5f;

	//Used to check if the player is walking left.
	private bool mWalkingLeft = false;

	//Used to check if the player is walking right.
	private bool mWalkingRight = false;

	//Checks if the player is grounded. If so, the player can jump.
	private bool mGrounded = true;

    //The direction the player is initially facing.
    private bool mFacingRight = true;

    //Checks if the player is currently firing bullets.
    private bool mIsFiringBullets = false;

	//Checks if the player has taken damage by an enemy.
	private bool mIsHurt = false;

	//Checks if the player is dead.
	private bool mIsDead = false;

	//Checks if the player is being knocked back.
	private bool mIsBeingKnockedBack = false;

    //The firing key.
    private KeyCode mFiringKey;

	//The main camera object.
	private GameObject mCameraObject;

	//The camera script for the main camera.
	private camera mCameraScript;

	//The camera transform component for the main camera.
	private Transform mCameraTransform;

    //The global game data.
    private globalData mGlobalData;

	//The layer mask of the collidable objects.
	private LayerMask collidableLayerMask;

	//The local transform of the object.
	private Transform mTransform;

	//The local rigid body of the object.
	private Rigidbody2D mRigidBody2D;

	//The sprite renderer.
	private SpriteRenderer mSpriteRenderer;

    //Public variables:

    //The prefab for the bullet object.
    public GameObject mBulletPrefab;
}
