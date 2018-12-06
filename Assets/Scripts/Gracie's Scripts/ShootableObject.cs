using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableObject : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "projectile") {
			 // gameObject.GetComponent<shooting>().score++;
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Rigidbody2D>();
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
