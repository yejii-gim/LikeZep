using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : BaseManager<UIManager>
{
    public static UIManager Instance;
    [SerializeField] private Image itemSlot;


    private void Awake()
    {
        Instance = this;
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
}
