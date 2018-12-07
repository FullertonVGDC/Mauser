using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDebris : MonoBehaviour
{
    Vector2 velocity;
    float gravity = 10;

    void Start()
    {
        velocity = new Vector2(Random.Range(-3f, 3f), Random.Range(2f, 4f));
        LeanTween.alpha(gameObject, 0, 1).setEase(LeanTweenType.easeInCubic).setOnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    void Update()
    {
        velocity.y -= gravity * Time.deltaTime;
        transform.Translate(velocity * Time.deltaTime);
        if (transform.position.y < 0)
        {
            velocity.y = 0;
            transform.position = new Vector2(transform.position.x, 0);
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