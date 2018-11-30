using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCamera : MonoBehaviour
{
    void Start()
    {
        transform.position = new Vector2(0, 4);
    }

    void Update()
    {

    }

    void GenerateBees()
    {
        /*float numberOfRows = 3;
        float spacingBetweenRows = 0.75f;
        float spacingBetweenBees = 1.25f;

        Vector2 bottomLeftCameraPoint = cameraComp.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 bottomRightCameraPoint = cameraComp.ScreenToWorldPoint(new Vector2(cameraComp.pixelWidth, 0));
        Vector2 beeSpawnPos = bottomLeftCameraPoint;

        float degCounter = 0;

        for (int i = 0; i < numberOfRows; i++)
        {
            while (beeSpawnPos.x < bottomRightCameraPoint.x)
            {
                GameObject newBee = Instantiate(mBeePrefab, beeSpawnPos, Quaternion.identity);
                newBee.transform.parent = mMainCamera.transform;
                newBee.GetComponent<Bee>().deg = degCounter;
                newBee.GetComponent<Bee>().anchorPos = beeSpawnPos;
                newBee.GetComponent<SpriteRenderer>().sortingOrder = i;

                beeSpawnPos.x += spacingBetweenBees;
                degCounter += 10;
            }

            beeSpawnPos.x = bottomLeftCameraPoint.x;
            beeSpawnPos.y += spacingBetweenRows;
        }*/
    }
}