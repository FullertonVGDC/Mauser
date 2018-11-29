using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	// Use this for initialization
	void Start()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
	    mTransform = GetComponent<Transform>();
	    mCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
		mAudioSourceOfCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update()
    {
        Vector2 bulletVelocity = new Vector2(mBulletSpeed, 0.0f);

        if (!mFacingRight)
        {
            bulletVelocity.x *= -1.0f;
        }

        mRigidBody.velocity = bulletVelocity;

		CheckIfOutsideCamera ();
	}

	void CheckIfOutsideCamera()
	{
		if (mTransform.position.x < mCameraTransform.position.x - 10.0f ||
			mTransform.position.x > mCameraTransform.position.x + 10.0f ||
			mTransform.position.y > mCameraTransform.position.y + 10.0f ||
			mTransform.position.y < mCameraTransform.position.y - 10.0f) 
		{
			Destroy (this.gameObject);
		}
	}
        
    //Collision Callbacks (Trigger).
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "collidable")
        {
            Destroy(this.gameObject);
        }

    	if (collider.gameObject.tag == "enemy1")
    	{	
			if(!collider.gameObject.GetComponent<DustBunny>().GetIsDead())
			{
				mAudioSourceOfCamera.PlayOneShot(mRubberBandHitEnemyAudioClip, 1.0f);
				Destroy(this.gameObject);
			}
    	}
		
		if (collider.gameObject.tag == "spider")
    	{
			if(!collider.gameObject.GetComponent<Spider>().GetIsDead())
			{
				mAudioSourceOfCamera.PlayOneShot(mRubberBandHitEnemyAudioClip, 1.0f);
				Destroy(this.gameObject);
			}
    	}
    }
	
	public void Pause()
	{
		enabled = false;
		mRigidBody.gravityScale = 0.0f;
		mPausedVelocity = mRigidBody.velocity;
		mRigidBody.velocity = new Vector2(0.0f, 0.0f);
	}
	
	public void UnPause()
	{
		enabled = true;
		mRigidBody.gravityScale = 1.0f;
		mRigidBody.velocity = mPausedVelocity;
	}

    //Setters:
    public void SetFacingRight(bool sFacingRight)
    {
        mFacingRight = sFacingRight;
        if (mFacingRight)
            GetComponent<SpriteRenderer>().flipX = false;
        else
            GetComponent<SpriteRenderer>().flipX = true;
    }

    //Getters:
    public bool GetFacingRight()
    {
        return mFacingRight;
    }
        
    //Checks if the bullet is facing to the right.
    private bool mFacingRight;

    //Bullet speeds.
    private float mBulletSpeed = 16.0f;
	
	//The paused velocity of the game object.
	private Vector2 mPausedVelocity;

    //The rigid body of the bullet object.
    private Rigidbody2D mRigidBody;

	//The transform component of the shot.
	private Transform mTransform;

	//The transform component of the camera.
	private Transform mCameraTransform;
	
	//The audio source of the camera component object.
	private AudioSource mAudioSourceOfCamera;
	
	//Public variables.
	
	//The sound clip that is played when the rubber band hits the enemy.
	public AudioClip mRubberBandHitEnemyAudioClip;
}
