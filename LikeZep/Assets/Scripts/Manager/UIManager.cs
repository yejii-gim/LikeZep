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
    [Header("Clothes")]
    [SerializeField] private GameObject clothesPanel;
    [SerializeField] private GameObject needCoinPanel;
    [SerializeField] private UIClothes clothesUI;
    public static int coinCount = 10;
    
    private void Awake()
    {
        Instance = this;
        coinText.text = coinCount.ToString();
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

    public void ToggleClothesPanel()
    {
        clothesUI.UpdateClothesButtons();
        clothesPanel.SetActive(!clothesPanel.activeSelf);
    }

    public void ToggleNeedCoinPanel()
    {
        needCoinPanel.SetActive(!needCoinPanel.activeSelf);
    }
}
