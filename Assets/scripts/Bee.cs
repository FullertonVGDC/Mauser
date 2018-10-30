using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    [HideInInspector]
    public float deg;
    float swaySpeed = 300;
    [HideInInspector]
    public Vector2 anchorPos;

    void Start()
    {
        anchorPos = transform.localPosition;
    }

    void Update()
    {
        deg += swaySpeed * Time.deltaTime;
        if (deg >= 720) deg -= 720;

        float finalSinX = 0.5f * Mathf.Sin(0.5f * deg * Mathf.Deg2Rad);
        float finalSinY = 0.35f * Mathf.Sin(deg * Mathf.Deg2Rad);
        transform.localPosition = new Vector3(anchorPos.x + finalSinX, anchorPos.y + finalSinY, transform.localPosition.z);
    }
}