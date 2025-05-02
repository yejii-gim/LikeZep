using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;
    float offsetX;
    float offsetY;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        if (player == null)
            return;
        offsetX = transform.position.x - player.position.x;
        offsetY = transform.position.y - player.position.y;
    }

    private void Update()
    {
        if (player == null)
            return;
        Vector3 pos = transform.position;
        pos.x = player.position.x + offsetX;
        pos.y = player.position.y + offsetY; 
        transform.position = pos;
    }
}
