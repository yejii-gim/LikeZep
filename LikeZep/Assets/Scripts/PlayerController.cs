using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : BaseController
{
    [Header("Ground")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [Header("Jump")]
    [SerializeField] private float jumpHeight = 1.5f; // 점프 최대 높이
    [SerializeField] private float jumpForce = 6f; // 점프 강도
    [SerializeField] private float jumpDuration = 0.5f; // 총 점프시간
    [SerializeField] private float moveSpeed = 2f; // 
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private int bulletIndex = 0;
    [SerializeField] private float spread = 0f;

    
    private GameManager gameManager;
    private Camera mainCamera;
    private float lastFireTime;
    private Vector3 startPos;
    private float timer;
    private bool isJumping;
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            StartCoroutine(JumpRoutine());
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public void Init(GameManager gameManager)
    {
        mainCamera = Camera.main;
        this.gameManager = gameManager;
        
    }
    protected override void HandleAction()
    {
        // 키보드 입력을 통해 이동 방향 계산 (좌/우/상/하)
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D 또는 ←/→
        float vertical = Input.GetAxisRaw("Vertical"); // W/S 또는 ↑/↓

        // 방향 벡터 정규화 (대각선일 때 속도 보정)
        movementDirection = new Vector2(horizontal, vertical).normalized;

        // 마우스 위치를 화면 좌표 → 월드 좌표로 변환
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPos = mainCamera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        // 현재 위치로부터 마우스 위치까지의 방향 계산
        if (lookDirection.magnitude < .9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
    }

    private void TryShoot()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= lastFireTime + fireRate)
        {
            if (lookDirection != Vector2.zero)
            {
                Vector2 direction = lookDirection.normalized;
                ProjectileMananger.Instance.ShootBullet(bulletIndex, this.transform.position, direction,true);
                lastFireTime = Time.time;
            }
        }
    }

    public override void Death()
    {
        base.Death();
        gameManager.GameOver();
    }

    private IEnumerator JumpRoutine()
    {
        isJumping = true;
        startPos = transform.position;
        timer = 0f;

        while (timer < jumpDuration)
        {
            timer += Time.deltaTime;
            float t = timer / jumpDuration;

            // 포물선 형태의 점프 곡선 (y축 위아래로만 시각 효과)
            float heightOffset = 4 * jumpHeight * t * (1 - t);
            transform.position = new Vector3(startPos.x, startPos.y + heightOffset, startPos.z);

            yield return null;
        }

        transform.position = startPos;
        isJumping = false;
    }
}
