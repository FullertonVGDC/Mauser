using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePortal : MonoBehaviour
{
    Player player;
    bool activated;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!activated)
            {
                if (!player.GetIsDead())
                {
                    activated = true;

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