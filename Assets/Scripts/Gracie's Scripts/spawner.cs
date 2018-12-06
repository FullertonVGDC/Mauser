using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject fallingObject;

    float x_val;
    float y_val;
    public float launchPower;

    public float launchX;
    public float launchY;

    public float destroyTime;



    void randomValues()
    {
        x_val = Random.value * launchX;
        y_val = Random.value * launchY;
    }

    public void obj_initialize()
    {
        GameObject objects = Instantiate(fallingObject, transform.position, Quaternion.identity);
        randomValues();
        objects.GetComponent<Rigidbody2D>().AddForce(new Vector2(x_val, y_val) * launchPower);
        Destroy(objects, destroyTime);
    }
}