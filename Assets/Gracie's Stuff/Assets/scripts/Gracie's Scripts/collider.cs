using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D bullet) { 		
		if (bullet.gameObject.tag == "object") {		
			Destroy(bullet.gameObject);
			shooting.score += 10;
		}
	} 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
