using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 8f;
    public float lowPosY = -3f;

    public float holeSizeMin = 5f;
    public float holeSizeMax = 8f;

    public Transform topObject;
    public Transform bottomObject;

    public float widthPadding = 4f;

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2f;

        topObject.localPosition = new Vector3(0, 7);
        bottomObject.localPosition = new Vector3(0, -7);

        Vector3 placePostion = lastPosition + new Vector3(widthPadding, 0);
        placePostion.y = Random.Range(lowPosY, highPosY);

        transform.position = placePostion;

        return placePostion;
    }
}
