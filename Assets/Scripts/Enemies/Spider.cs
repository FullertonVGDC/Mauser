using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		mTransform = GetComponent<Transform> ();
		mRigidBody2D = GetComponent<Rigidbody2D>();
        mCollider2D = GetComponent<Collider2D>();
		mAudioSourceOfCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        mAnimator = GetComponent<Animator>();

		collidableLayerMask = LayerMask.GetMask ("collidable");
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	//Use this for anything involving collision or anything else that is discrete every 1 / 60th of a frame.
	void FixedUpdate()
	{
        if (!mDead)
        {
			if(!mResting && !mClimbingDown)
			{
				PrimaryMovement();
				CheckForWalls();
			}
			else if(mResting)
			{
				CheckIfPlayerInRange();
			}
			else if(mClimbingDown)
			{
				ClimbingDownMovement();
			}
        }
        else
        {
            DyingAnimation();
        }
		
        CheckIfBelowDeathLine();
	}

    //Collision Callbacks (Trigger).
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "bullet" && !mDead)
        {
            mCurHealth -= 1;

            if (mCurHealth == 0)
            {
                mDead = true;
                mJustDied = true;
				
				mAudioSourceOfCamera.PlayOneShot(mDeathAudioClip, 1.0f);
            }

            //The other bullet being attacted by.
            Bullet bullet1 = collider.GetComponent<Bullet>();

            //Find out which direction the spider is being attacked from.
            if (bullet1.GetFacingRight())
            {
                mAttackedFromLeft = false;
            }
            else
            {
                mAttackedFromLeft = true;
            }
        }
    }

    void PrimaryMovement()
    {
        //Choose the walking direction speed and direction.
        if(mFacingLeft == true)
        {
            mRigidBody2D.velocity = new Vector3(-mCurrentWalkingSpeed, mRigidBody2D.velocity.y, 0.0f);
        }
        else
        {
            mRigidBody2D.velocity = new Vector3(mCurrentWalkingSpeed, mRigidBody2D.velocity.y, 0.0f);
        }

        //Check if grounded.
        if(mRigidBody2D.velocity.y <= 0.0f)
        {
            //Perform a raycast below the spider to check if the spider is grounded.

            //The Left ray.
            RaycastHit2D groundRayL = Physics2D.Raycast(mTransform.position - new Vector3(0.3f, 0.1f, 0.0f), 
                new Vector3(0.0f, -1.0f, 0.0f), 1.0f, collidableLayerMask);

            //The right ray.
            RaycastHit2D groundRayR = Physics2D.Raycast(mTransform.position - new Vector3(-0.3f, 0.1f, 0.0f), 
                new Vector3(0.0f, -1.0f, 0.0f), 1.0f, collidableLayerMask);

            //If the collision occurs, check if grounded. If not, assume the spider is in the air and set grounded to 
            // false.
            if (groundRayL.collider != null)
            {
                float collisionDistance = mTransform.position.y - 0.75f - groundRayL.point.y;

                mGrounded = true;
                mTransform.position = new Vector3(mTransform.position.x, 
                    mTransform.position.y - collisionDistance, 
                    mTransform.position.z);
                mRigidBody2D.velocity = new Vector2(mRigidBody2D.velocity.x, 0.0f);
                mRigidBody2D.gravityScale = 0.0f;
            } 
            else if (groundRayR.collider != null) 
            {
                float collisionDistance = mTransform.position.y - 0.75f - groundRayR.point.y;

                mGrounded = true;
                mTransform.position = new Vector3(mTransform.position.x, 
                    mTransform.position.y - collisionDistance, 
                    mTransform.position.z);
                mRigidBody2D.velocity = new Vector2(mRigidBody2D.velocity.x, 0.0f);
                mRigidBody2D.gravityScale = 0.0f;
            } 
            else 
            {
                mGrounded = false;
                mRigidBody2D.gravityScale = 4.0f;
            }

            //Check if the spider is changing directions.
            if(mGrounded)
            {
                if(groundRayL.collider == null)
                {
                    mFacingLeft = false;
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else if(groundRayR.collider == null)
                {
                    mFacingLeft = true;
                    GetComponent<SpriteRenderer>().flipX = false;
                }
            }
        }
    }
	
	void ClimbingDownMovement()
	{
		mRigidBody2D.velocity = new Vector2(0.0f, -4.0f);
		
		//Stretch the web object as the spider moves downwards.
		
		//The web game object that belongs to the spider.
		GameObject webObject = null;
		
		//Find the web object and assign it to the webObject.
		foreach (Transform child in transform.parent)
		{
			if(child.gameObject.name == "Web")
			{
				webObject = child.gameObject;
			}
		}
		
		//Just to be safe.
		if(webObject != null)
		{
			webObject.transform.localScale = new Vector3(
				webObject.transform.localScale.x, 
				webObject.transform.localScale.y + (4.0f * Time.deltaTime), 
				webObject.transform.localScale.z);
		}
			
		//Perform a raycast below the spider to check if the spider is grounded.

		//The Left ray.
		RaycastHit2D groundRayL = Physics2D.Raycast(mTransform.position - new Vector3(0.3f, 0.1f, 0.0f), 
			new Vector3(0.0f, -1.0f, 0.0f), 1.0f, collidableLayerMask);

		//The right ray.
		RaycastHit2D groundRayR = Physics2D.Raycast(mTransform.position - new Vector3(-0.3f, 0.1f, 0.0f), 
			new Vector3(0.0f, -1.0f, 0.0f), 1.0f, collidableLayerMask);

		//If the collision occurs, check if grounded. If not, assume the spider is in the air and set grounded to 
		// false. Also set is climbing down to false once the spider touches the ground.
		if (groundRayL.collider != null) 
		{
			float collisionDistance = mTransform.position.y - 0.75f - groundRayL.point.y;

			mGrounded = true;
            mAnimator.SetTrigger("Landing Trigger");

            mClimbingDown = false;
			mTransform.position = new Vector3(mTransform.position.x, 
				mTransform.position.y - collisionDistance, 
				mTransform.position.z);
			mRigidBody2D.velocity = new Vector2(mRigidBody2D.velocity.x, 0.0f);
		} 
		else if (groundRayR.collider != null) 
		{
			float collisionDistance = mTransform.position.y - 0.75f - groundRayR.point.y;

			mGrounded = true;
            mAnimator.SetTrigger("Landing Trigger");

            mClimbingDown = false;
			mTransform.position = new Vector3(mTransform.position.x, 
				mTransform.position.y - collisionDistance, 
				mTransform.position.z);
			mRigidBody2D.velocity = new Vector2(mRigidBody2D.velocity.x, 0.0f);
		} 
		else 
		{
			mGrounded = false;
		}
	}

    void CheckForWalls()
    {
        //The Left ray.
        RaycastHit2D wallRayL = Physics2D.Raycast(mTransform.position - new Vector3(-0.0f, 0.1f, 0.0f), 
            new Vector3(-1.0f, 0.0f, 0.0f), 0.5f, collidableLayerMask);

        //The right ray.
        RaycastHit2D wallRayR = Physics2D.Raycast(mTransform.position - new Vector3(0.0f, 0.1f, 0.0f), 
            new Vector3(1.0f, 0.0f, 0.0f), 0.5f, collidableLayerMask);

        if (wallRayL.collider != null)
        {
            mFacingLeft = false;
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (wallRayR.collider != null)
        {
            mFacingLeft = true;
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void DyingAnimation()
    {
        transform.Rotate(new Vector3(0, 0, mDeathRotateAngle));
        //If just died, fire the spider into the air and have it fall off screen.
        // Also prevent the spider from colliding with walls.
        if (mJustDied)
        {
            //Vector2 prevVelocity = new Vector2(mRigidBody2D.velocity.x, mRigidBody2D.velocity.y);

            if (mAttackedFromLeft)
            {
                mRigidBody2D.velocity = new Vector2(-4.0f, 10.0f);
            }
            else
            {
                mRigidBody2D.velocity = new Vector2(4.0f, 10.0f);
            }

            mRigidBody2D.gravityScale = 2.0f;
            mCollider2D.isTrigger = true;
            mAnimator.Play("Death");

            if (GameObject.FindGameObjectWithTag("Player").transform.position.x < transform.position.x)
                mDeathRotateAngle = Random.Range(-15f, -5f);
            else
                mDeathRotateAngle = Random.Range(5f, 15f);

            //lol
            if (Random.Range(1, 101) == 100)
                mDeathRotateAngle = 50f;

            mJustDied = false;
        }
    }
	
	void CheckIfPlayerInRange()
	{
		//Check if the player is within the box function. If so, set resting
		// to false, and set climbing down to true.
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
		
		if(playerObj.transform.position.y < (transform.position.y - 2.0f) &&
			playerObj.transform.position.x < (transform.position.x + 2.5f) &&
			playerObj.transform.position.x > (transform.position.x - 2.5f))
		{
			mResting = false;
			mClimbingDown = true;
            mAnimator.SetTrigger("Descent Trigger");

            //Check which direction the spider should face.
            if (playerObj.transform.position.x < transform.position.x)
			{
				mFacingLeft = true;
			}
			else
			{
				mFacingLeft = false;
			}
		}			
	}

    void CheckIfBelowDeathLine()
    {
        //If below the death line, destroy the spider.
        if (mTransform.position.y < -5.0f) 
        {
            Destroy(gameObject);
        }
    }

    public void StartMoving()
    {
        mCurrentWalkingSpeed = mMaxWalkingSpeed;
    }

	public void Pause()
	{
		enabled = false;
		mRigidBody2D.gravityScale = 0.0f;
		mPausedVelocity = mRigidBody2D.velocity;
		mRigidBody2D.velocity = new Vector2(0.0f, 0.0f);
        mAnimator.enabled = false;
	}
	
	public void UnPause()
	{
		enabled = true;
		
		//Only set the gravity if the spider is dead.
		if(mDead)
		{
			mRigidBody2D.gravityScale = 2.0f;
		}
		
		mRigidBody2D.velocity = mPausedVelocity;
        mAnimator.enabled = true;
	}
	
    //Getters:

    public bool GetIsDead()
    {
        return mDead;
    }

	//The previous distance from the ground for the left ray.
	//private float mPrevDistanceFromGroundL = 999.0f;

	//The previous distance from the ground for the right ray.
	//private float mPrevDistanceFromGroundR = 999.0f;

    //The speed by which the player walks.
	float mMaxWalkingSpeed = 5.0f;

    //The current speed of the spider
    float mCurrentWalkingSpeed;

    //The current health of the spider.
    private int mCurHealth = 5;

    float mDeathRotateAngle;

    //The maximum health of the spider.
    //private int mMaxHealth = 5;

	//Checks if the player is grounded. If so, the player can jump.
	private bool mGrounded = false;

	//Checks if the spider is facing left.
	private bool mFacingLeft = true;

    //Checks if the spider has been attacked from the left.
    private bool mAttackedFromLeft = false;
	
	//Checks if the spider is resting.
	private bool mResting = true;
	
	//Checks if the spider is climbing down.
	private bool mClimbingDown = false;

    //Checks if the spider is dead.
    private bool mDead = false;

    //Checks if the spider has just died this frame.
    private bool mJustDied = true;
	
	//The paused velocity of the game object.
	private Vector2 mPausedVelocity;

	//The layer mask of the collidable objects.
	private LayerMask collidableLayerMask;

	//The local transform of the object.
	private Transform mTransform;

	//The local rigid body of the object.
	private Rigidbody2D mRigidBody2D;

    //The collider for the spider object.
    private Collider2D mCollider2D;
	
	//The audio source of the camera component object.
	private AudioSource mAudioSourceOfCamera;

    //The animator
    Animator mAnimator;
	
	//Public variables.
	
	//The sound clip that is played when the spider enemy dies.
	public AudioClip mDeathAudioClip;
}
