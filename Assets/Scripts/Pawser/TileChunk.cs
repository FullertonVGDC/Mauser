using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChunk : MonoBehaviour
{
    [HideInInspector]
    public float chunkSpeed;

	int mLeanTweenActiveAnimID1;

	int mLeanTweenActiveAnimID2;

    void Start()
    {
        transform.localScale = new Vector2(1, 0);

		//The current lean tween animation being created.
		LTDescr curLeanTweenAnim;

		curLeanTweenAnim = LeanTween.scaleY (gameObject, 1, chunkSpeed);
		curLeanTweenAnim.setEase (LeanTweenType.easeOutCubic);

		curLeanTweenAnim.setOnComplete(() =>
        {
			LTDescr curLeanTweenAnim2 = LeanTween.scaleY(gameObject, 0, chunkSpeed);
			curLeanTweenAnim2.setEase(LeanTweenType.easeInCubic);
			curLeanTweenAnim2.setOnComplete(() =>
            {
                Destroy(gameObject);
            });

			mLeanTweenActiveAnimID2 = curLeanTweenAnim2.id;
        });

		mLeanTweenActiveAnimID1 = curLeanTweenAnim.id;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().TakeDamage();
        }
    }

	public void Pause()
	{
		enabled = false;

		if(mLeanTweenActiveAnimID1 != -1)
		{
			LeanTween.pause(mLeanTweenActiveAnimID1);
		}

		if(mLeanTweenActiveAnimID2 != -1)
		{
			LeanTween.pause(mLeanTweenActiveAnimID2);
		}
	}

	public void UnPause()
	{
		enabled = true;

		if(mLeanTweenActiveAnimID1 != -1)
		{
			LeanTween.resume(mLeanTweenActiveAnimID1);
		}

		if(mLeanTweenActiveAnimID2 != -1)
		{
			LeanTween.resume(mLeanTweenActiveAnimID2);
		}
	}
}