using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDebris : MonoBehaviour
{
    Vector2 velocity;
    float gravity = 10;

	int mLeanTweenActiveAnimID1;

    void Start()
    {
        velocity = new Vector2(Random.Range(-3f, 3f), Random.Range(2f, 4f));

		//The current lean tween animation being created.
		LTDescr mLeanTweenDescr = LeanTween.alpha (gameObject, 0, 1);
		mLeanTweenDescr.setEase (LeanTweenType.easeInCubic);
		mLeanTweenDescr.setOnComplete(() =>
        {
            Destroy(gameObject);
        });

		mLeanTweenActiveAnimID1 = mLeanTweenDescr.id;
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

		if(mLeanTweenActiveAnimID1 != -1)
		{
			LeanTween.pause(mLeanTweenActiveAnimID1);
		}
	}

	public void UnPause()
	{
		enabled = true;

		if(mLeanTweenActiveAnimID1 != -1)
		{
			LeanTween.resume(mLeanTweenActiveAnimID1);
		}
	}
}