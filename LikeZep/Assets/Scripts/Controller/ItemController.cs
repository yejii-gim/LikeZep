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

    // 보물상자 열었을때 1초뒤에 삭제
    public void OpenChest()
    {
        //animationHandler.Open(); // 예: 열리는 애니메이션
        Destroy(gameObject, 1f); // 1초 후 제거
        UIManager.Instance.CoinSpawn(this.transform);
    }
}
