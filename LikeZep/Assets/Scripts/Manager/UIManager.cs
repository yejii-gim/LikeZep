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
    [Header("Riding")]
    [SerializeField] private GameObject ridingPanel;
    [SerializeField] public GameObject charcterRiding;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float rideOffsetY = 0.2f;

    public static int coinCount = 10;

    bool ridingActive = false;
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
        clothesPanel.SetActive(!clothesPanel.activeSelf);
    }

    public void ToggleNeedCoinPanel()
    {
        needCoinPanel.SetActive(!needCoinPanel.activeSelf);
    }

    public void ToggleRidingPanel()
    {
        ridingPanel.SetActive(!ridingPanel.activeSelf);
    }

    public void ToggleRiding()
    {
        ridingActive = charcterRiding.activeSelf;
        StartCoroutine(SmoothRidingTransition(!ridingActive));
    }

    private IEnumerator SmoothRidingTransition(bool riding)
    {
        float duration = 0.2f;
        float elapsed = 0f;
        float offset = riding ? rideOffsetY : -rideOffsetY;

        Vector3 startPos = playerTransform.position;
        Vector3 targetPos = startPos + new Vector3(0f, offset, 0f);

        if (riding) charcterRiding.SetActive(true);
        if (!riding) charcterRiding.SetActive(false);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            playerTransform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        playerTransform.position = targetPos;
    }
}
