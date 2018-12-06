using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePortal : MonoBehaviour
{
    Player player;
    bool canTouch;
    bool activated;
    public float rotationSpeed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(gameObject, new Vector3(2, 2, 2), 2).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
        {
            canTouch = true;
        });

        LeanTween.value(rotationSpeed * 20, rotationSpeed, 2).setEase(LeanTweenType.easeOutCubic).setOnUpdate((float val) =>
        {
            rotationSpeed = val;
        });

        LeanTween.delayedCall(6, () =>
        {
            LeanTween.scale(gameObject, new Vector3(0, 0, 0), 8).setEase(LeanTweenType.easeInOutCubic).setOnComplete(() =>
            {
                Destroy(gameObject);
            });
        });
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!activated && canTouch)
            {
                if (!player.GetIsDead())
                {
                    activated = true;
                    LeanTween.cancelAll();

                    GameObject.Find("GuiFader(Clone)").GetComponent<GuiFader>().SetIsFadingIn(true);
                    player.GetComponent<AudioSource>().PlayOneShot(player.mMauserFinishLevelAudioClip, 1.0f);

                    player.SetFoundMinigame(true);
                    player.GetComponent<SpriteRenderer>().enabled = false;
                    player.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
                }
            }
        }
    }
}