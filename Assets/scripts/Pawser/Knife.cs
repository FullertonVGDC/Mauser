using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public KnifeHandler kh;

    public void StabDown()
    {
        LTSeq seq = LeanTween.sequence();
        seq.append(LeanTween.moveLocalY(gameObject, kh.startStabPosY + 1, 0.25f).setEase(LeanTweenType.easeOutCubic));
        seq.append(LeanTween.moveLocalY(gameObject, kh.endStabPosY, 0.25f).setEase(LeanTweenType.easeInCubic));
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
}