using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRiding : UIBase
{
    [SerializeField] private GameObject[] riding; // 변경할 라이딩 오브젝트들
    [SerializeField] private Image[] ridingImages; // 보여지는 라이딩 이미지들
    [SerializeField] private Animator rideAnimator;
    [SerializeField] private RuntimeAnimatorController[] rideAnimators; // 라이딩 애니메이터컨트롤러들

    private SpriteRenderer ridingSprite;
    private void Start()
    {
        // 1. SpriteRenderer 초기화
        ridingSprite = UIManager.Instance.charcterRiding.GetComponent<SpriteRenderer>();
        if (ridingSprite == null)
        {
            ridingSprite = UIManager.Instance.charcterRiding.AddComponent<SpriteRenderer>();
        }

        // 초기화 호출
        InitButtons(currentIndex, riding, ridingImages,ridingSprite,rideAnimator,rideAnimators);

        // 버튼 리스너 설정
        UpdateButtons(riding, ridingImages, ridingSprite, rideAnimator, rideAnimators, 10);
    }
}
