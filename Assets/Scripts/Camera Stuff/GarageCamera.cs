using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageCamera : MonoBehaviour
{
    Bounds camBounds;
    public Player player;

    void Start()
    {
        camBounds.SetMinMax(new Vector3(-1.0f, 5.0f, transform.position.z), new Vector3(161.0f, 13.0f, transform.position.z));
        transform.position = new Vector3(0, 4, transform.position.z);
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        KeepCameraInHorizontalBounds();
        KeepCameraInVerticalBounds();
    }



    void KeepCameraInHorizontalBounds()
    {
        if (transform.position.x < camBounds.min.x)
            transform.position = new Vector3(camBounds.min.x, transform.position.y, transform.position.z);
        else if (transform.position.x > camBounds.max.x)
            transform.position = new Vector3(camBounds.max.x, transform.position.y, transform.position.z);
    }

    void KeepCameraInVerticalBounds()
    {
        if (transform.position.y < camBounds.min.y)
            transform.position = new Vector3(transform.position.x, camBounds.min.y, transform.position.z);
        else if (transform.position.y > camBounds.max.y)
            transform.position = new Vector3(transform.position.x, camBounds.max.y, transform.position.z);
    }
}