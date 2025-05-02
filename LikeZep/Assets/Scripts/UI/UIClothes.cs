using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIClothes : UIBase
{
    [SerializeField] private GameObject[] clothes;
    [SerializeField] private Image[] clothesImages;
    [SerializeField] private SpriteRenderer characterSprite;
    [SerializeField] private Animator charcterAnimator;
    [SerializeField] private RuntimeAnimatorController[] npcAnimators;


    private void Start()
    {
        // �ʱ�ȭ ȣ��
        InitButtons(currentIndex, clothes, clothesImages, characterSprite, charcterAnimator, npcAnimators);

        // ��ư ������ ����
        UpdateButtons(clothes, clothesImages, characterSprite, charcterAnimator, npcAnimators, 5);
    }

}
