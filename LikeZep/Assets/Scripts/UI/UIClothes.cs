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
        // 초기화 호출
        InitButtons(currentIndex, clothes, clothesImages, characterSprite, charcterAnimator, npcAnimators);

        // 버튼 리스너 설정
        UpdateButtons(clothes, clothesImages, characterSprite, charcterAnimator, npcAnimators, 5);
    }

}
