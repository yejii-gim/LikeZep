using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : BaseManager<UIManager>
{
    public static UIManager Instance;
    [SerializeField] private Image itemSlot;
    [SerializeField] private Button riding;
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private GameObject coinPrefab;

    public static int coinCount;
    private void Awake()
    {
        Instance = this;
        coinText.text = "0";
    }

    public void ChangeItemSlot(ItemData item)
    {
        Sprite itemSprite = item.icon;

        if (itemSprite != null)
        {
            itemSlot.sprite = itemSprite;
        }
    }

    public void ClearItemSlot()
    {
        itemSlot.sprite = null;
    }

    public void CoinSpawn(Transform position)
    {
        Instantiate(coinPrefab, position.position, Quaternion.identity);
    }

    public void CoinUpdate()
    {
        coinText.text = coinCount.ToString();
    }
}
