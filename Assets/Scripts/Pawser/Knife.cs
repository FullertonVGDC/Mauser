﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public KnifeHandler kh;
    public GameObject tileDebris;

    public void StabDown()
    {
        LTSeq seq = LeanTween.sequence();
        seq.append(LeanTween.moveLocalY(gameObject, kh.startStabPosY + 1, 0.25f).setEase(LeanTweenType.easeOutCubic));
        seq.append(LeanTween.moveLocalY(gameObject, kh.endStabPosY, 0.25f).setEase(LeanTweenType.easeInCubic).setOnComplete(() =>
        {
            for (int i = 0; i < 5; i++)
                Instantiate(tileDebris, new Vector2(transform.position.x, 0), Quaternion.identity);
            //Instantiate(tileDebris, new Vector2(transform.position.x, transform.position.y - (GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2)), Quaternion.identity);
        }));
        seq.append(LeanTween.moveLocalY(gameObject, kh.startStabPosY, 0.5f).setEase(LeanTweenType.easeOutCubic));
        seq.append(() =>
        {
            if (!kh.finishedPanning)
                StabDown();
        });
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
		LeanTween.pauseAll();
	}
	
	public void UnPause()
	{
		enabled = true;
		LeanTween.resumeAll();
	}
}