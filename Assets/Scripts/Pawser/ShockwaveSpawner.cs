using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveSpawner : MonoBehaviour
{
    public float xVelocity;
    public float duration;
    public float spawnTimerLength;
    float spawnTimer;
    float durationTimer;

    public GameObject tileChunk;
    public LayerMask chunkCheckLayer;
    public float chunkSpeed;

    void Start()
    {
        spawnTimer = spawnTimerLength;
        durationTimer = duration;
    }

    void Update()
    {
        transform.Translate(new Vector2(xVelocity, 0) * Time.deltaTime);

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer += spawnTimerLength;
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, Mathf.Infinity, chunkCheckLayer);
            if (hit)
            {
                GameObject newChunk = Instantiate(tileChunk, new Vector2(transform.position.x, transform.position.y - hit.distance), Quaternion.identity);
                newChunk.GetComponent<TileChunk>().chunkSpeed = chunkSpeed;
            }
        }

        durationTimer -= Time.deltaTime;
        if (durationTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "collidable")
        {
            Destroy(gameObject);
        }
    }
}