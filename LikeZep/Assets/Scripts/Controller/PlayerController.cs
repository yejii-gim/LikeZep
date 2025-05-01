using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [Header("Bullet")]
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private int bulletIndex = 0;
    [SerializeField] private float spread = 0f;

    private NPCController currentNPC;
    private ItemController currentItem;
    private GameManager gameManager;
    private Camera mainCamera;
    private float lastFireTime;
    private Vector3 startPos;
    private float timer;
    private bool isJumping;
    DialogueLine line;
    GameObject item;
    bool isItem = false;
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            StartCoroutine(Jump());
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TryShoot();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isItem)
                DialogueManager.Instance.ShowDialogue(currentNPC.DialoguePanel, currentNPC.MessageText, line.isQuest ? line.isQuesting : line.firstMeeting);
            else
            {
                UIManager.Instance.ChangeItemSlot(currentItem.Item);
                currentItem.OpenChest();
            }
        }
    }

    protected override void FixedUpdate()
    {
        Movement(movementDirection);
    }
    public void Init(GameManager gameManager)
    {
        mainCamera = Camera.main;
        this.gameManager = gameManager;

    }
    protected override void HandleAction()
    {
        // 키보드 입력을 통해 이동 방향 계산 (좌/우/상/하)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 방향 벡터 정규화 (대각선일 때 속도 보정)
        movementDirection = new Vector2(horizontal, vertical).normalized;

        if (movementDirection != Vector2.zero)
            lookDirection = movementDirection;
    }

    private void TryShoot()
    {
        if (Time.time >= lastFireTime + fireRate)
        {
            if (lookDirection != Vector2.zero)
            {
                Vector2 direction = lookDirection.normalized;
                ProjectileMananger.Instance.ShootBullet(bulletIndex, this.transform.position, direction, true);
                lastFireTime = Time.time;
            }
        }
    }

    public override void Death()
    {
        base.Death();
    }

    private IEnumerator Jump()
    {
        isJumping = true;
        startPos = transform.position;
        timer = 0f;

        while (timer < jumpDuration)
        {
            timer += Time.deltaTime;
            float t = timer / jumpDuration;

            float heightOffset = 4 * jumpHeight * t * (1 - t);
            transform.position = new Vector3(startPos.x, startPos.y + heightOffset, startPos.z);

            yield return null;
        }

        transform.position = startPos;
        isJumping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            NPCController npc = collision.GetComponentInParent<NPCController>();
            if (npc != null)
            {
                currentNPC = npc;
                var npcName = currentNPC.name;
                line = DialogueManager.Instance.dialogueLines.Find(d => d.npcName == npcName);  
                npc.DialoguePanel.SetActive(true);
                if (!line.isQuest)
                {
                    ItemManager.Instance.SpawnChest();
                    line.isQuest = true;
                }
                
                if(isItem)
                {
                    currentNPC.NPCCompleted();
                    isItem = false;
                }
            }
        }
        if(collision.CompareTag("Item"))
        {
            ItemController item = collision.gameObject.GetComponent<ItemController>();
            currentItem = item; 

            isItem = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            NPCController npc = collision.GetComponentInParent<NPCController>();
            if (npc == currentNPC)
            {
                npc.DialoguePanel.SetActive(false);
                DialogueManager.Instance.HideDialogue(npc.DialoguePanel);
                currentNPC = null;
                line.isQuest = true;
            }
        }
    }

}