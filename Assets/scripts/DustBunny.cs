using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustBunny : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		mTransform = GetComponent<Transform> ();
		mRigidBody2D = GetComponent<Rigidbody2D> ();
        mCollider2D = GetComponent<Collider2D>();
        mSpriteRenderer = GetComponent<SpriteRenderer>();
        mAnimator = GetComponent<Animator>();

		collidableLayerMask = LayerMask.GetMask ("collidable");
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (!mDead)
        {
            PrimaryMovement();
            CheckForWalls();
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
                mSpriteRenderer.flipX = false;
                mRigidBody2D.velocity = Vector3.zero;
                mAnimator.SetTrigger("Death Trigger");
            }
        }
    }

    void PrimaryMovement()
    {
        //Choose the walking direction speed and direction.
        if(mFacingLeft == true)
        {
            mRigidBody2D.velocity = new Vector3(-mWalkingSpeed, mRigidBody2D.velocity.y, 0.0f);
            mSpriteRenderer.flipX = false;
        }
        else
        {
            mRigidBody2D.velocity = new Vector3(mWalkingSpeed, mRigidBody2D.velocity.y, 0.0f);
            mSpriteRenderer.flipX = true;
        }

        //Check if grounded.
        if(mRigidBody2D.velocity.y <= 0.0f)
        {
            //Perform a raycast below the enemy to check if the enemy is grounded.

            //The Left ray.
            RaycastHit2D groundRayL = Physics2D.Raycast(mTransform.position - new Vector3(0.3f, 0.1f, 0.0f), 
                new Vector3(0.0f, -1.0f, 0.0f), 1.0f, collidableLayerMask);

            //The right ray.
            RaycastHit2D groundRayR = Physics2D.Raycast(mTransform.position - new Vector3(-0.3f, 0.1f, 0.0f), 
                new Vector3(0.0f, -1.0f, 0.0f), 1.0f, collidableLayerMask);

            //If the collision occurs, check if grounded. If not, assume the enemy is in the air and set grounded to 
            // false.
            if (groundRayL.collider != null) 
            {
                float collisionDistance = mTransform.position.y - 0.5f - groundRayL.point.y;

                mGrounded = true;
                mTransform.position = new Vector3(mTransform.position.x, 
                    mTransform.position.y - collisionDistance, 
                    mTransform.position.z);
                mRigidBody2D.velocity = new Vector2(mRigidBody2D.velocity.x, 0.0f);
                mRigidBody2D.gravityScale = 0.0f;
            } 
            else if (groundRayR.collider != null) 
            {
                float collisionDistance = mTransform.position.y - 0.5f - groundRayR.point.y;

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

            //Check if the enemy is changing directions.
            if(mGrounded)
            {
                if(groundRayL.collider == null)
                    mFacingLeft = false;
                else if(groundRayR.collider == null)
                    mFacingLeft = true;
            }
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
            mFacingLeft = false;

        if (wallRayR.collider != null)
            mFacingLeft = true;
    }

    void CheckIfBelowDeathLine()
    {
        //If below the death line, destroy the enemy.
        if (mTransform.position.y < -5.0f) 
        {
            Destroy(gameObject);
        }
    }

    void DeleteSelf()
    {
        Destroy(gameObject);
    }

    //Getters:

    public bool GetIsDead()
    {
        return mDead;
    }

	//The previous distance from the ground for the left ray.
	private float mPrevDistanceFromGroundL = 999.0f;

	//The previous distance from the ground for the right ray.
	private float mPrevDistanceFromGroundR = 999.0f;

    //The speed by which the player walks.
	private float mWalkingSpeed = 2.0f;

    //The current health of the enemy.
    private int mCurHealth = 3;

    //The maximum health of the enemy.
    private int mMaxHealth = 3;

	//Checks if the player is grounded. If so, the player can jump.
	private bool mGrounded = false;

	//Checks if the enemy is facing left.
	private bool mFacingLeft = true;

    //Checks if the enemy is dead.
    private bool mDead = false;

	//The layer mask of the collidable objects.
	private LayerMask collidableLayerMask;

	//The local transform of the object.
	private Transform mTransform;

	//The local rigid body of the object.
	private Rigidbody2D mRigidBody2D;

    //The collider for the enemy object.
    private Collider2D mCollider2D;

    //The sprite renderer for the dust bunny.
    private SpriteRenderer mSpriteRenderer;

    //The animator component for the dust bunny.
    private Animator mAnimator;
}
