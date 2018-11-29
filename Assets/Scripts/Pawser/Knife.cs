using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
	//The knife handler object, needed to check if the stabbing animation has finished. Also provides basic knife position info.
    public KnifeHandler kh;
	
	//The tile debres prefab used for spawning debris whenever the knife strikes the ground.
    public GameObject tileDebris;
	
	//The ids for the LeanTween animations in the animation sequence for the knife animations.
	private int id1;
	private int id2; 
	private int id3;
	
	//The currently active animation sequence animation. Numbered 0-2.
	private int curEnabledAnimInSequence = -1;

	//The animation for the knife stabbing. It is split into 3 parts: rising up a little, striking down, then rising back up again.
    public void StabDown()
    {
		//The lean tween sequence of animations for the knife.
        LTSeq seq = LeanTween.sequence();
		
		//The current lean tween animation being created and added to the sequence.
		LTDescr curLeanTweenAnim;
		
		//First, make the knife rise a little bit upwards to prepare to strike.
		curLeanTweenAnim = LeanTween.moveLocalY(gameObject, kh.startStabPosY + 1, 0.25f);
		curLeanTweenAnim.setEase(LeanTweenType.easeOutCubic);
		curLeanTweenAnim.setOnComplete(() => 
		{
			curEnabledAnimInSequence = 1;
			
			LeanTween.pause(id1);
			LeanTween.pause(id3);
			
			LeanTween.resume(id2);
		});
		id1 = curLeanTweenAnim.id;
        seq.append(curLeanTweenAnim);
		
		//Then fly downwards and hit the ground, spawning debris.
		curLeanTweenAnim = LeanTween.moveLocalY(gameObject, kh.endStabPosY, 0.25f);
		curLeanTweenAnim.setEase(LeanTweenType.easeInCubic);
		curLeanTweenAnim.setOnComplete(() =>
        {
			//Spawn 5 debris objects once the knife has hit the ground.
            for (int i = 0; i < 5; i++)
			{
                Instantiate(tileDebris, new Vector2(transform.position.x, 0), Quaternion.identity);
			}
			
			LeanTween.pause(id1);
			LeanTween.pause(id2);
			
			LeanTween.resume(id3);
		
			curEnabledAnimInSequence = 2;
            //Instantiate(tileDebris, new Vector2(transform.position.x, transform.position.y - (GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2)), Quaternion.identity);
        });
		id2 = curLeanTweenAnim.id;
        seq.append(curLeanTweenAnim);
		
		//Then fly back upwards back to the starting position.
		curLeanTweenAnim = LeanTween.moveLocalY(gameObject, kh.startStabPosY, 0.5f);
		curLeanTweenAnim.setEase(LeanTweenType.easeOutCubic);
		curLeanTweenAnim.setOnComplete(() =>
		{
			curEnabledAnimInSequence = -1;
            if (!kh.finishedPanning)
                StabDown();
        });
		id3 = curLeanTweenAnim.id;
        seq.append(curLeanTweenAnim);
		
		curEnabledAnimInSequence = 0;
		
		LeanTween.pause(id2);
		LeanTween.pause(id3);
    }

	//The function for checking if the knife is colliding with trigger collision objects.
    void OnTriggerEnter2D(Collider2D other)
    {
		//If colliding with the player, deal damage to the player.
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().TakeDamage();
        }
    }
	
	//The pause function which will freeze the knife animation sequence.
	public void Pause()
	{
		switch(curEnabledAnimInSequence)
		{
			case 0 : LeanTween.pause(id1);
				break;
			case 1 : LeanTween.pause(id2);
				break;
			case 2 : LeanTween.pause(id3);
				break;
			default : 
				break;
		}
		
		enabled = false;
	}
	
	public void UnPause()
	{
		switch(curEnabledAnimInSequence)
		{
			case 0 : LeanTween.resume(id1);
				break;
			case 1 : LeanTween.resume(id2);
				break;
			case 2 : LeanTween.resume(id3);
				break;
			default : 
				break;
		}
		
		enabled = true;
	}
}