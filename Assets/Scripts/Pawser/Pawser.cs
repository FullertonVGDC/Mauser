using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pawser : MonoBehaviour
{
    public enum State { Idle, KnifeAttack, FishAttack, HairballAttack, Dying };
    [HideInInspector]
    public State state;

    public int health;
    int maxHealth;
    float damageTintStrength;

    public float idleTimerMinLength;
    public float idleTimerMaxLength;
    float timer;

    Animator animator;
    SpriteRenderer sr;
    Image gfImage;

    public Image healthBar;

    public GameObject knifeHandlerPrefab;
    public GameObject shockwaveSpawnerPrefab;
    public GameObject hairballPrefab;



    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        gfImage = GameObject.Find("GuiFader(Clone)").GetComponent<Image>();

        state = State.Idle;
        timer = Random.Range(idleTimerMinLength, idleTimerMaxLength);
        maxHealth = health;
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
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = Random.Range(idleTimerMinLength, idleTimerMaxLength);

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

            case State.Dying:
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "bullet" && state != State.Dying)
        {
            Destroy(other.gameObject);
            damageTintStrength = 0.15f;
            health--;
            healthBar.fillAmount = health / (float)maxHealth;

            if (health <= 0)
            {
                state = State.Dying;
                animator.Play("Dying");
                LeanTween.delayedCall(5, () =>
                {
                    LeanTween.value(0, 1, 3).setOnUpdate((float val) =>
                    {
                        gfImage.color = new Color(gfImage.color.r, gfImage.color.g, gfImage.color.b, val);
                    }).setOnComplete(() =>
                    {
                        GlobalData.instance.StopMusic();
                        SceneManager.LoadScene("credits");
                    });
                });
            }
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
}