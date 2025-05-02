using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRiding : UIBase
{
    [SerializeField] private GameObject[] riding; // ������ ���̵� ������Ʈ��
    [SerializeField] private Image[] ridingImages; // �������� ���̵� �̹�����
    [SerializeField] private Animator rideAnimator;
    [SerializeField] private RuntimeAnimatorController[] rideAnimators; // ���̵� �ִϸ�������Ʈ�ѷ���

    private SpriteRenderer ridingSprite;
    private void Start()
    {
        // 1. SpriteRenderer �ʱ�ȭ
        ridingSprite = UIManager.Instance.charcterRiding.GetComponent<SpriteRenderer>();
        if (ridingSprite == null)
        {
            ridingSprite = UIManager.Instance.charcterRiding.AddComponent<SpriteRenderer>();
        }

        // �ʱ�ȭ ȣ��
        InitButtons(currentIndex, riding, ridingImages,ridingSprite,rideAnimator,rideAnimators);

        // ��ư ������ ����
        UpdateButtons(riding, ridingImages, ridingSprite, rideAnimator, rideAnimators, 10);
    }
}
