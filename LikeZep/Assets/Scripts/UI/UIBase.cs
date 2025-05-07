using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    protected bool[] isPurchase;
    protected int currentIndex = 0;
    protected Color selectedColor = Color.yellow;
    protected Color defaultColor = Color.white;
    protected void InitButtons(int index, GameObject[] gameObjects, Image[] Images, SpriteRenderer sprite, Animator animator, RuntimeAnimatorController[] animators)
    {
        isPurchase = new bool[gameObjects.Length];
        isPurchase[0] = true;

        currentIndex = 0;

        ResetButtonColors(gameObjects);
        UpdateButtons(gameObjects,Images,sprite,animator,animators);
    }
    protected void ChangeSprite(GameObject[] gameObjects,int index, Image[] Images, SpriteRenderer sprite, Animator animator, RuntimeAnimatorController[] animators)
    {
        currentIndex = index;
        Sprite newSprite = Images[index].sprite;
        sprite.sprite = newSprite;
        animator.runtimeAnimatorController = animators[index];

        ResetButtonColors(gameObjects);
    }

    // 버튼들 색 리셋
    protected void ResetButtonColors(GameObject[] gameObjects)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            Image img = gameObjects[i].GetComponent<Image>();

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

    // 구매할때
    protected void TryPurchaseClothing(int index, GameObject[] gameObjects,Image[] Images, SpriteRenderer sprite, Animator animator, RuntimeAnimatorController[] animators,int price)
    {
        if (UIManager.coinCount < price)
        {
            UIManager.Instance.ToggleNeedCoinPanel();
            return;
        }
        UIManager.coinCount -= price;
        UIManager.Instance.CoinUpdate();

        isPurchase[index] = true;

        Button button = gameObjects[index].GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => ChangeSprite(gameObjects,index,Images,sprite,animator,animators));

        // 바로 착용하도록
        ChangeSprite(gameObjects, index, Images, sprite, animator, animators);
    }

    // 버튼들 리스너 설정
    protected void UpdateButtons(GameObject[] gameObjects, Image[] Images, SpriteRenderer sprite, Animator animator, RuntimeAnimatorController[] animators,int price = 5)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            int index = i;
            Button button = gameObjects[i].GetComponent<Button>();
            Image img = gameObjects[i].GetComponent<Image>();

            button.onClick.RemoveAllListeners();

            if (isPurchase[i])
            {
                button.onClick.AddListener(() => ChangeSprite(gameObjects, index, Images, sprite, animator, animators));
            }
            else if (UIManager.coinCount >= price)
            {
                button.onClick.AddListener(() => TryPurchaseClothing(index, gameObjects, Images, sprite, animator, animators,price));
            }
            else
            {
                button.onClick.AddListener(() => UIManager.Instance.ToggleNeedCoinPanel());
            }
        }
        ResetButtonColors(gameObjects);
    }
    protected bool IsSelected(int index)
    {
        return index == currentIndex;
    }
}
