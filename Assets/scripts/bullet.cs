using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
        mRigidBody = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector2 bulletVelocity = new Vector2(mBulletSpeed, 0.0f);

        if (!mFacingRight)
        {
            bulletVelocity.x *= -1.0f;
        }

        mRigidBody.velocity = bulletVelocity;
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
			Destroy(this.gameObject);
			Destroy(collider.gameObject);
		}
    }

    //Setters:
    public void SetFacingRight(bool sFacingRight)
    {
        mFacingRight = sFacingRight;
    }
        
    //Checks if the bullet is facing to the right.
    private bool mFacingRight;

    //Bullet speeds.
    private float mBulletSpeed = 16.0f;

    //The rigid body of the bullet object.
    private Rigidbody2D mRigidBody;
}
