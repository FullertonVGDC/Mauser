using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawser : MonoBehaviour
{
    public enum State { Idle, KnifeAttack, FishAttack, HairballAttack };
    [HideInInspector]
    public State state;

    public float idleTimerMinLength;
    public float idleTimerMaxLength;
    float idleTimer;

    Animator animator;
    public GameObject knifeHandlerPrefab;
    public GameObject shockwaveSpawnerPrefab;



    void Start()
    {
        animator = GetComponent<Animator>();

        state = State.Idle;
        idleTimer = Random.Range(idleTimerMinLength, idleTimerMaxLength);
    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                idleTimer -= Time.deltaTime;
                if (idleTimer <= 0)
                {
                    idleTimer = Random.Range(idleTimerMinLength, idleTimerMaxLength);

                    //state = (State)Random.Range(1, 4);
                    //state = State.KnifeAttack;
                    state = State.FishAttack;
                    if (state == State.KnifeAttack)
                    {
                        animator.Play("Knife Throw");
                    }
                    else if (state == State.FishAttack)
                    {
                        animator.Play("Fish Slam");
                    }
                    else if (state == State.HairballAttack)
                    {

                    }
                }
                break;

            case State.KnifeAttack:
                //I don't think we do anything here because the knife entities handle everything lol
                break;

            case State.FishAttack:
                //Also don't need anything here lol
                break;

            case State.HairballAttack:
                break;
        }
    }

    public void ResetToIdleState()
    {
        state = State.Idle;
    }

    public void SpawnKnives()
    {
        GameObject knifeHandler = Instantiate(knifeHandlerPrefab);
        knifeHandler.GetComponent<KnifeHandler>().pawser = this;
    }

    public void SpawnShockwaves()
    {
        Instantiate(shockwaveSpawnerPrefab, new Vector2(7.5f, 2), Quaternion.identity);
        GameObject leftShockwave = Instantiate(shockwaveSpawnerPrefab, new Vector2(7.5f, 2), Quaternion.identity);
        leftShockwave.GetComponent<ShockwaveSpawner>().xVelocity = -leftShockwave.GetComponent<ShockwaveSpawner>().xVelocity;
    }


	
	public void Pause()
	{
		enabled = false;
		animator.enabled = false;
	}
	
	public void UnPause()
	{
		enabled = true;
		animator.enabled = true;
	}
}