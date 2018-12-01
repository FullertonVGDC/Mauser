using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class shooting : MonoBehaviour {

	Rigidbody2D rb;
	public GameObject bullet;
	public float bulletSpeed; 

	// for bullet
	float timer; 
	bool facingRight = true; 
	public int x;
	public int y;

	// for movement
	public int topX; 

	// minigame ui
	public float minigame_timer; 
	public static int score = 0;
	public Text timerText;
	public Text scoreText;

	// functions
	void movement() {
		if (Input.GetKeyDown(KeyCode.Space) && timer > 1f) {
			shoot ();
			timer = 0;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			rb.AddForce(new Vector2(x, 0));
			facingRight = true;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			rb.AddForce (new Vector2 (-x, 0));
			facingRight = false;
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			rb.AddForce (new Vector2(0, y));
		}
	}

	void shoot() { // shoots the object
		if (facingRight) {
			GameObject shot = (GameObject)Instantiate (bullet, transform.position, transform.rotation);
			shot.GetComponent<Rigidbody2D> ().velocity = shot.transform.right * bulletSpeed;
			Destroy (shot, 2.0f);
		} else { 
			GameObject shot = (GameObject)Instantiate (bullet, transform.position, transform.rotation);
			shot.GetComponent<Rigidbody2D>().velocity = shot.transform.right * -bulletSpeed;
			Destroy(shot, 2.0f);
		}
	}

	void switch_scenes() {					// once the game timer reaches 30, it loads the prize screen with
		if (minigame_timer > 30.0f) {		// 100 points, otherwise it will show the lose screen
			if (score >= 100) {
				SceneManager.LoadScene ("prize_screen");
			} else {
				SceneManager.LoadScene ("minigame_lose_screen");
			}
		}
	}
		
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();

		minigame_timer = 0;
		score = 0; // score is added from the bullet object with the collider script
		timerText.text = "Timer: " + minigame_timer.ToString ();
		scoreText.text = "Score: " + score.ToString ();
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime; // for bullet
		minigame_timer += Time.deltaTime;
		timerText.text = "Timer: " + ((int)minigame_timer).ToString ();
		scoreText.text = "Score: " + score.ToString ();
		movement ();
		switch_scenes ();
	}
}
	
/*
 bullet = bullet object with collider script
 bullet speed = 20
 x = 10
 y = 250
 top x = 0
 minigame timer = 0
 timer text = timer text
 score text = scoring text
*/