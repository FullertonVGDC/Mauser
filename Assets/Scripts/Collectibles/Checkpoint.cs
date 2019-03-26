using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Player player;
    bool activated;
    public Color activatedColor;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!activated)
            {
                if (!player.GetIsDead())
                {
                    GlobalData.instance.SetCheckpointEnabled(true);
                    GlobalData.instance.SetCheckpointPosition(transform.position);

                    GlobalData.instance.SetSavedCurrency(GlobalData.instance.GetCurrency());
                    player.GetComponent<AudioSource>().PlayOneShot(player.mMauserCollectCheckpointAudioClip, 1.0f);

                    GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
                    activated = true;

                    if (GlobalData.instance.GetCurrency() >= 999 && !GlobalData.instance.GetHasBeenToMinigame())
                    {
                        Instantiate(player.mMinigamePortalPrefab, new Vector2(transform.position.x, transform.position.y + 2.0f), Quaternion.identity);
                    }
                }
            }
        }
    }
}