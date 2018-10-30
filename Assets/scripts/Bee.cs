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
        if (deg >= 360) deg -= 360;

        float finalSin = 0.5f * Mathf.Sin(deg * Mathf.Deg2Rad);
        transform.localPosition = new Vector3(anchorPos.x, anchorPos.y + finalSin, transform.localPosition.z);
    }
}