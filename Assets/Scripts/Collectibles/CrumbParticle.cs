using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumbParticle : MonoBehaviour
{
    Vector2 velocity;
    float gravity;
    bool fadingOut;

    void Start()
    {
        velocity = new Vector2(0, 0);
        gravity = Random.Range(5f, 10f);
    }

    void Update()
    {
        if (!fadingOut)
        {
            velocity.y -= (gravity * Time.deltaTime);
            transform.Translate(velocity * Time.deltaTime);
        }

        if (transform.position.y < -10) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "collidable")
        {
            fadingOut = true;
            LeanTween.alpha(gameObject, 0, 1).setOnComplete(() => { Destroy(gameObject); });
        }
    }
}