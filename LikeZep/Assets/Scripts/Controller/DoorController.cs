using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject doorOpened;
    [SerializeField] private GameObject doorClosed;
    [SerializeField] private GameObject doorPanel;
    [SerializeField] private Transform playerPosition;

    public GameObject DoorPanel => doorPanel;
    public void DoorOpen()
    {
        doorOpened.SetActive(true);
        doorClosed.SetActive(false);
    }

    public void DoorClose()
    {
        doorOpened.SetActive(false);
        doorClosed.SetActive(true);
    }
}
