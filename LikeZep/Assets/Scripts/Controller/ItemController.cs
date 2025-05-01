using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private GameObject talkPanel;
    [SerializeField] private ItemData item;
    AnimationHandler animationHandler;
    public ItemData Item => item;
    private void Awake()
    {
        animationHandler = GetComponentInChildren<AnimationHandler>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            talkPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            talkPanel.SetActive(false);
        }
    }

    // �������� �������� 1�ʵڿ� ����
    public void OpenChest()
    {
        //animationHandler.Open(); // ��: ������ �ִϸ��̼�
        Destroy(gameObject, 1f); // 1�� �� ����
        UIManager.Instance.CoinSpawn(this.transform);
    }
}
