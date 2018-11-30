using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circle : MonoBehaviour {

	Rigidbody2D rb;
	bool pause = false; 

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {

		/*if (Input.GetKey (KeyCode.Escape)) {
            Time.timeScale = 0;
            pause = true;
        }

        if (Input.GetKey (KeyCode.Space)) {
            Time.timeScale = 1;
        }*/

		if (Input.GetKey(KeyCode.RightArrow)) {
			rb.AddForce(new Vector2(5, 0));
		}

		if (Input.GetKey(KeyCode.LeftArrow)) {
			rb.AddForce(new Vector2(-5, 0));
		}
	}
}

// Time.timeScale = 0;
// canvas, buttons, images, text, delete, open
// esc
