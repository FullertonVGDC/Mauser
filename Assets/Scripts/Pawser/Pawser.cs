﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawser : MonoBehaviour
{
    public enum State { Idle, KnifeAttack, FishAttack, HairballAttack };
    [HideInInspector]
    public State state;

    public int health = 100;
    float damageTintStrength;

    public float idleTimerMinLength;
    public float idleTimerMaxLength;
    float idleTimer;

    Animator animator;
    SpriteRenderer sr;

    public GameObject knifeHandlerPrefab;
    public GameObject shockwaveSpawnerPrefab;
    public GameObject hairballPrefab;



    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        state = State.Idle;
        idleTimer = Random.Range(idleTimerMinLength, idleTimerMaxLength);
    }

    void Update()
    {
        //Handle damage tinting
        if (damageTintStrength > 0) damageTintStrength -= (1f * Time.deltaTime);
        else damageTintStrength = 0;
        sr.color = new Color(1, 1 - damageTintStrength, 1 - damageTintStrength);

        //Process behaviour of whatever state pawser is in
        switch (state)
        {
            case State.Idle:
                idleTimer -= Time.deltaTime;
                if (idleTimer <= 0)
                {
                    idleTimer = Random.Range(idleTimerMinLength, idleTimerMaxLength);

                    state = (State)Random.Range(1, 4);
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
                        animator.Play("Hairball");
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
                //</'J'>/
                break;
        }
    }



    public void SetStateToIdle()
    {
        state = State.Idle;
    }

    public void SpawnKnives()
    {
        Instantiate(knifeHandlerPrefab);
    }

    public void SpawnShockwaves()
    {
        Instantiate(shockwaveSpawnerPrefab, new Vector2(7.5f, 2), Quaternion.identity);
        GameObject leftShockwave = Instantiate(shockwaveSpawnerPrefab, new Vector2(7.5f, 2), Quaternion.identity);
        leftShockwave.GetComponent<ShockwaveSpawner>().xVelocity = -leftShockwave.GetComponent<ShockwaveSpawner>().xVelocity;
    }

    public void SpawnHairball()
    {
        if (Random.Range(0, 2) == 0)
        {
            GameObject leftHairball = Instantiate(hairballPrefab);
            leftHairball.GetComponent<Hairball>().velocity = new Vector2(-4, 20);
            GameObject rightHairball = Instantiate(hairballPrefab);
            rightHairball.GetComponent<Hairball>().velocity = new Vector2(4, 20);
        }
        else
        {
            GameObject leftHairball = Instantiate(hairballPrefab);
            leftHairball.GetComponent<Hairball>().velocity = new Vector2(-6, 20);
            GameObject middleHairball = Instantiate(hairballPrefab);
            middleHairball.GetComponent<Hairball>().velocity = new Vector2(0, 20);
            GameObject rightHairball = Instantiate(hairballPrefab);
            rightHairball.GetComponent<Hairball>().velocity = new Vector2(6, 20);
        }
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


    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "bullet")
        {
            health--;
            damageTintStrength = 0.1f;
            Destroy(other.gameObject);
        }
    }
}