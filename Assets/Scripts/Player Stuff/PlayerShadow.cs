using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public GameObject[] shadowPieces;
    public LayerMask layerShadowCanCollideWith;

    void Start()
    {
        float scaleX = transform.localScale.x;
        shadowPieces[0].transform.Translate(new Vector2(-(shadowPieces[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x * (shadowPieces.Length / 2f)) * scaleX, 0));
        for (int i = 1; i < shadowPieces.Length; i++)
        {
            float spriteWidth = shadowPieces[i - 1].GetComponent<SpriteRenderer>().sprite.bounds.size.x * scaleX;
            shadowPieces[i].transform.position = new Vector2(shadowPieces[i - 1].transform.position.x + spriteWidth, shadowPieces[i - 1].transform.position.y);
        }
    }

    void Update()
    {
        for (int i = 0; i < shadowPieces.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(shadowPieces[i].transform.position.x, transform.position.y), Vector2.down, Mathf.Infinity, layerShadowCanCollideWith);
            if (hit)
            {
                shadowPieces[i].transform.position = new Vector2(shadowPieces[i].transform.position.x, transform.position.y - hit.distance);
            }
            else
            {
                shadowPieces[i].transform.position = new Vector2(shadowPieces[i].transform.position.x, -100);
            }
        }
    }
}