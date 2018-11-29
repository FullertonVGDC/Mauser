using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDustParticle : MonoBehaviour
{
    [HideInInspector]
    public Vector2 velocity;
    float gravity;

    void Start()
    {
        gravity = Random.Range(5f, 10f);
        LeanTween.alpha(gameObject, 0, 1).setOnComplete(() => { Destroy(gameObject); });
    }

    void Update()
    {
        velocity.y -= (gravity * Time.deltaTime);
        transform.Translate(velocity * Time.deltaTime);
        if (transform.position.y < -10) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Only detect collisions if the particle is moving downwards
        //This is so the particle can spawn inside a collider and move up out of it
        if (velocity.y < 0) velocity.y = Mathf.Abs(velocity.y);
    }
}