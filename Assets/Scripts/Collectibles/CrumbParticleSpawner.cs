using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumbParticleSpawner : MonoBehaviour
{
    public Sprite[] crumbParticles;
    public float spawnTimerMin;
    public float spawnTimerMax;
    float spawnTimer;

    public GameObject crumbPrefab;

    void Start()
    {
        spawnTimer = Random.Range(spawnTimerMin, spawnTimerMax);
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer += Random.Range(spawnTimerMin, spawnTimerMax);
            Vector2 spawnPos = transform.position;
            spawnPos += Random.insideUnitCircle * 0.3f;
            GameObject newCrumb = Instantiate(crumbPrefab, spawnPos, Quaternion.identity);
            newCrumb.GetComponent<SpriteRenderer>().sprite = crumbParticles[Random.Range(0, crumbParticles.Length)];
        }
    }
}