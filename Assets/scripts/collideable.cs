using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collideable : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
		//Get the sprite renderer.
        mSpriteRenderer = this.GetComponent<SpriteRenderer>();

        //Make the sprite renderer invisible.
        mSpriteRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    //The sprite renderer of the collision object.
    private SpriteRenderer mSpriteRenderer;
}
