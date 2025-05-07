using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float highPosY = 8f;
    [SerializeField] private float lowPosY = -3f;
    [SerializeField][Range(0f, 1f)] private float coinSpawnChance = 0.3f;

    public Transform topObject;
    public Transform bottomObject;

    public float widthPadding = 4f;

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        topObject.localPosition = new Vector3(0, 7);
        bottomObject.localPosition = new Vector3(0, -7);

        Vector3 placePostion = lastPosition + new Vector3(widthPadding, 0);
        placePostion.y = Random.Range(lowPosY, highPosY);

        transform.position = placePostion;

        // 코인 랜덤으로 생성
        if (Random.value < coinSpawnChance)
        {
            float middleY = (topObject.position.y + bottomObject.position.y) / 2f;
            Vector3 spawnPos = new Vector3(transform.position.x, middleY, 0f);
            UIManager.Instance.CoinSpawn(spawnPos);
        }


        return placePostion;
    }
}
