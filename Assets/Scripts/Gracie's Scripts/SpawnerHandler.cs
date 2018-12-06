using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHandler : MonoBehaviour
{
    enum State { Intro, Active };
    State state = State.Intro;
    float timer;

    public float introTimerLength = 2;

    public Spawner[] spawners;
    public float spawnTimerLength = 0.5f;



    void Start()
    {
        timer = introTimerLength;
    }

    void Update()
    {
        switch (state)
        {
            case State.Intro:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    state = State.Active;
                    timer = spawnTimerLength;
                    spawners[Random.Range(0, spawners.Length)].obj_initialize();
                }
                break;

            case State.Active:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer += spawnTimerLength;
                    spawners[Random.Range(0, spawners.Length)].obj_initialize();
                }
                break;
        }
    }
}