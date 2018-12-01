using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_m: MonoBehaviour {
	Rigidbody2D rb;
	public int x; // 10
	public int y; // 10
	public int topX; // 10
	public float timer;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.RightArrow)) {
			rb.AddForce(new Vector2(x, 0));
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			rb.AddForce (new Vector2 (-x, 0));
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			rb.AddForce (new Vector2 (0, y));
		}
		if (rb.velocity.x > 5) // controls the speed
		{
			rb.velocity = new Vector2(topX, rb.velocity.y);
		}
		else if (rb.velocity.x < -5)
			rb.velocity = new Vector2(topX, rb.velocity.y);

	} // update bracket

}
