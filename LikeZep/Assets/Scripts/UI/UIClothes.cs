using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIClothes : MonoBehaviour
{
    [SerializeField] private GameObject[] clothes;
    [SerializeField] private Image[] clothesImages;
    [SerializeField] private SpriteRenderer characterSprite;
    [SerializeField] private Animator charcterAnimator;
    [SerializeField] private RuntimeAnimatorController[] npcAnimators;
    [SerializeField] private Color selectedColor = Color.yellow;
    [SerializeField] private Color defaultColor = Color.white;

    private bool[] isPurchase;
    private int currentIndex = 0;
    private void Start()
    {
        InitClothesButtons();       
    }
    // �ʱ� ����
    private void InitClothesButtons()
    {
        isPurchase = new bool[clothes.Length];
        isPurchase[0] = true;

        currentIndex = 0;

        ResetButtonColors();
        UpdateClothesButtons();
    }
    private void ChangeCharacterSprite(int index)
    {
        currentIndex = index;
        Sprite newSprite = clothesImages[index].sprite;
        characterSprite.sprite = newSprite;
        charcterAnimator.runtimeAnimatorController = npcAnimators[index];

        ResetButtonColors();
    }

    // ��ư�� �� ����
    private void ResetButtonColors()
    {
        for (int i = 0; i < clothes.Length; i++)
        {
            Image img = clothes[i].GetComponent<Image>();

            if (!isPurchase[i])
            {
                img.color = new Color(1f, 1f, 1f, 0.5f);
            }
            else if (IsSelected(i))
            {
                img.color = selectedColor;
            }
            else
            {
                img.color = defaultColor;
            }
        }
    }

    // �����Ҷ� 5���̻� �ִ� ���
    private void TryPurchaseClothing(int index)
    {
        if(UIManager.coinCount < 5)
        {
            UIManager.Instance.ToggleNeedCoinPanel();
            return;
        }
        UIManager.coinCount -= 5;
        UIManager.Instance.CoinUpdate();

        isPurchase[index] = true;

        Button button = clothes[index].GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => ChangeCharacterSprite(index));

        // �ٷ� �����ϵ���
        ChangeCharacterSprite(index);
    }

    // clothes ��ư�� ������ ����
    public void UpdateClothesButtons()
    {
        for (int i = 0; i < clothes.Length; i++)
        {
            int index = i;
            Button button = clothes[i].GetComponent<Button>();
            Image img = clothes[i].GetComponent<Image>();

            button.onClick.RemoveAllListeners();

            if (isPurchase[i])
            {
                button.onClick.AddListener(() => ChangeCharacterSprite(index));
            }
            else if (UIManager.coinCount >= 5)
            {
                button.onClick.AddListener(() => TryPurchaseClothing(index));
            }
            else
            {
                button.onClick.AddListener(() => UIManager.Instance.ToggleNeedCoinPanel());
            }
        }
        ResetButtonColors();
    }
    private bool IsSelected(int index)
    {
        return index == currentIndex;
    }
}
