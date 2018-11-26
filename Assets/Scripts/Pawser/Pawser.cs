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
                    state = State.KnifeAttack;
                    if (state == State.KnifeAttack)
                    {
                        animator.Play("Knife Throw");
                    }
                    else if (state == State.FishAttack)
                    {

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
                break;

            case State.HairballAttack:
                break;
        }
    }

    public void SpawnKnives()
    {
        GameObject knifeHandler = Instantiate(knifeHandlerPrefab);
        knifeHandler.GetComponent<KnifeHandler>().pawser = this;
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