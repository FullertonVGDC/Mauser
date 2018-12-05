using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falling : MonoBehaviour
{
    public GameObject fallingObject;

    // location coordinates
    public float x_val;
    public float y_val;
    public float launchPower;

    // amount multipled for force for x, y, and p
    public float launchX;
    public float launchY;
    // public float launchP;

    // boundaries for minimum and maximum distance
    // public float minP;
    public float min;
    public float max;

    public float timer;
    public float spawnTime;
    public float destroyTime;


    void randomValues()
    {

        x_val = Random.value * launchX; // 25
        y_val = Random.value * launchY; // 20
                                        /*	while (launchPower < launchP) {
                                                launchPower = Random.value * launchP;
                                            } */
        while (y_val < min || y_val > max)
        { // 5 and 25
            y_val = Random.value * launchY;
        }
        while (x_val < min || x_val > max)
        {
            x_val = Random.value * launchX;
        }

    }

    void obj_initialize()
    {

        GameObject objects = (GameObject)Instantiate(fallingObject, transform.position, transform.rotation);
        randomValues();
        objects.GetComponent<Rigidbody2D>().AddForce(new Vector2(x_val, y_val) * launchPower);
        Destroy(objects, destroyTime);

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTime)
        {
            timer = 0;
            randomValues();
            obj_initialize();

        }

    }

}

/* x = 0
 * y = 0
 * launch power = 20
 * launch x = 25
 * launch y = 15
 * launch p = 20
 * min p = 10
 * min = 5
 * max = 25
 * timer = 0
 * spawn time = 3
 * destroy = 2
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * */
