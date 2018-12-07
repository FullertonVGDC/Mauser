using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hairball : MonoBehaviour
{
    public Vector2 velocity;
    public float gravity;

    void Update()
    {
        velocity.y -= gravity * Time.deltaTime;
        transform.Translate(velocity * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().TakeDamage();
        }

        if (other.gameObject.tag == "collidable")
        {
            Destroy(gameObject);
        }
    }

	public void Pause()
	{
		enabled = false;
	}

	public void UnPause()
	{
		enabled = true;
	}
}