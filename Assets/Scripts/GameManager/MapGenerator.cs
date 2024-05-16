using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private const float TILE_SIZE = 3.84f;

    public GameObject[] mapPrefabs;

    private float leftMostPosition = 0;
    private float rightMostPosition = 0;

    private float cameraLeftMostPosition = 0;
    private float cameraRightMostPosition = 0;

    void Update()
    {
        UpdateCamera();

        while (rightMostPosition < cameraRightMostPosition + TILE_SIZE)
        {
            GenerateTile(rightMostPosition);
        }

        while (leftMostPosition > cameraLeftMostPosition - TILE_SIZE)
        {
            GenerateTile(leftMostPosition - TILE_SIZE);
        }
    }

    private void GenerateTile(float xCoord) {
        int randomIndex = UnityEngine.Random.Range(0, mapPrefabs.Length);
        GameObject tile = Instantiate(mapPrefabs[randomIndex], transform);
        tile.transform.position = new Vector2(xCoord, 0);

        rightMostPosition = Math.Max(rightMostPosition, xCoord + TILE_SIZE);
        leftMostPosition = Math.Min(leftMostPosition, xCoord);
    }

    private void UpdateCamera()
    {
        cameraRightMostPosition = Math.Max(
            cameraRightMostPosition,
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x
        );
        cameraLeftMostPosition = Math.Min(
            cameraLeftMostPosition,
            Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x
        );
    }
}
