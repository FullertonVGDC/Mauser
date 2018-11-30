using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCamera : MonoBehaviour
{
    float leftBound = 2;
    float rightBound = 20;
    float velY;

    float[] speedChangeTimes = { 17f, 50f };
    float[] speeds = { 3, 5 };
    float speedChangeTimer;
    int currentSpeed = 0;

    public Player player;
    public GameObject beePrefab;
    CameraHandler cameraHandler;



    void Start()
    {
        cameraHandler = GetComponent<CameraHandler>();
        GenerateBees();

        LeanTween.value(velY, 1, 5).setOnUpdate((float value) =>
        {
            velY = value;
        });
    }

    void Update()
    {
        if (currentSpeed < speedChangeTimes.Length) CheckForSpeedUp();
        transform.Translate(Vector2.up * velY * Time.deltaTime);
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        KeepCameraInHorizontalBounds();
    }

    void CheckForSpeedUp()
    {
        speedChangeTimer += Time.deltaTime;
        if (speedChangeTimer >= speedChangeTimes[currentSpeed])
        {
            LeanTween.value(velY, speeds[currentSpeed], 2).setOnUpdate((float value) =>
            {
                velY = value;
            });
            currentSpeed++;
        }
    }

    void KeepCameraInHorizontalBounds()
    {
        if (transform.position.x < leftBound)
            transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);
        else if (transform.position.x > rightBound)
            transform.position = new Vector3(rightBound, transform.position.y, transform.position.z);
    }

    void GenerateBees()
    {
        float numberOfRows = 3;
        float spacingBetweenRows = 0.75f;
        float spacingBetweenBees = 1.25f;

        Vector2 bottomLeftCameraPoint = GetComponent<Camera>().ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 bottomRightCameraPoint = GetComponent<Camera>().ScreenToWorldPoint(new Vector2(GetComponent<Camera>().pixelWidth, 0));
        Vector2 beeSpawnPos = bottomLeftCameraPoint;

        float degCounter = 0;

        for (int i = 0; i < numberOfRows; i++)
        {
            while (beeSpawnPos.x < bottomRightCameraPoint.x)
            {
                GameObject newBee = Instantiate(beePrefab, beeSpawnPos, Quaternion.identity);
                newBee.transform.parent = transform;
                newBee.GetComponent<Bee>().deg = degCounter;
                newBee.GetComponent<Bee>().anchorPos = beeSpawnPos;
                newBee.GetComponent<SpriteRenderer>().sortingOrder = i;

                beeSpawnPos.x += spacingBetweenBees;
                degCounter += 10;
            }

            beeSpawnPos.x = bottomLeftCameraPoint.x;
            beeSpawnPos.y += spacingBetweenRows;
        }
    }
}