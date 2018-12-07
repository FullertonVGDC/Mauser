using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChunk : MonoBehaviour
{
    [HideInInspector]
    public float chunkSpeed;

    void Start()
    {
        transform.localScale = new Vector2(1, 0);
        LeanTween.scaleY(gameObject, 1, chunkSpeed).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
        {
            LeanTween.scaleY(gameObject, 0, chunkSpeed).setEase(LeanTweenType.easeInCubic).setOnComplete(() =>
            {
                Destroy(gameObject);
            });
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