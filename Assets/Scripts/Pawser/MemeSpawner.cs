using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemeSpawner : MonoBehaviour
{
    public Sprite[] memes;
    public SpriteRenderer leftMeme;
    public SpriteRenderer rightMeme;

    void Start()
    {
        leftMeme.sprite = memes[Random.Range(0, memes.Length - 1)];
        rightMeme.sprite = memes[Random.Range(0, memes.Length - 1)];
    }
}