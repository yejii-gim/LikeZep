using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    float offsetX;
    float offsetY;
    private bool isMiniGame = false;
    private void Awake()
    {
        var existing = FindObjectsOfType<CameraController>();
        if (existing.Length > 1)
        {
            Destroy(gameObject); // 이미 하나 있다면 자신 제거
            return;
        }
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

        if (!isMiniGame)
            pos.y = player.position.y + offsetY;

        transform.position = pos;

    }

    public void SetMiniGameMode(bool isMini)
    {
        isMiniGame = isMini;
    }

    public void ResetCameraPosition()
    {
        if (Camera.main != null)
        {
            Vector3 newPos = new Vector3(0f, 0f, Camera.main.transform.position.z);
            Camera.main.transform.position = newPos;
        }
    }
}
