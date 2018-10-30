using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBees : MonoBehaviour
{
    Camera mainCamera;
    public GameObject beePrefab;
    public float numberOfRows;
    public float spacingBetweenRows;
    public float spacingBetweenBees;

    void Start()
    {
        mainCamera = GetComponent<Camera>();

        Vector2 bottomLeftCameraPoint = mainCamera.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 bottomRightCameraPoint = mainCamera.ScreenToWorldPoint(new Vector2(mainCamera.pixelWidth, 0));
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

                beeSpawnPos.x += spacingBetweenBees;
                degCounter += 10;
            }
            beeSpawnPos.x = bottomLeftCameraPoint.x;
            beeSpawnPos.y += spacingBetweenRows;
        }
    }
}