using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawser : MonoBehaviour
{
    enum State { Idle, KnifeAttack, FishAttack, HairballAttack };
    State state;

    public float idleTimerMinLength;
    public float idleTimerMaxLength;
    float idleTimer;

    Animator animator;
    public GameObject knife;



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

                    state = (State)Random.Range(1, 4);
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
        GameObject knife1 = Instantiate(knife, new Vector2(7, 7), Quaternion.Euler(new Vector3(0, 0, 180)));
        knife1.GetComponent<Knife>().pawser = this;

        GameObject knife2 = Instantiate(knife, new Vector2(14, 7), Quaternion.Euler(new Vector3(0, 0, 180)));
        knife2.GetComponent<Knife>().pawser = this;
    }
}