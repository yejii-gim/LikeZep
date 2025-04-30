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
    [SerializeField] private float jumpHeight = 1.5f; // ���� �ִ� ����
    [SerializeField] private float jumpForce = 6f; // ���� ����
    [SerializeField] private float jumpDuration = 0.5f; // �� �����ð�
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
        // Ű���� �Է��� ���� �̵� ���� ��� (��/��/��/��)
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D �Ǵ� ��/��
        float vertical = Input.GetAxisRaw("Vertical"); // W/S �Ǵ� ��/��

        // ���� ���� ����ȭ (�밢���� �� �ӵ� ����)
        movementDirection = new Vector2(horizontal, vertical).normalized;

        // ���콺 ��ġ�� ȭ�� ��ǥ �� ���� ��ǥ�� ��ȯ
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPos = mainCamera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        // ���� ��ġ�κ��� ���콺 ��ġ������ ���� ���
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

            // ������ ������ ���� � (y�� ���Ʒ��θ� �ð� ȿ��)
            float heightOffset = 4 * jumpHeight * t * (1 - t);
            transform.position = new Vector3(startPos.x, startPos.y + heightOffset, startPos.z);

            yield return null;
        }

        transform.position = startPos;
        isJumping = false;
    }
}
