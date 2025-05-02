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
    [SerializeField] private float jumpHeight = 1.5f; // ���� �ִ� ����
    [SerializeField] private float jumpForce = 6f; // ���� ����
    [SerializeField] private float jumpDuration = 0.5f; // �� �����ð�
    [Header("Bullet")]
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private int bulletIndex = 0;
    [SerializeField] private float spread = 0f;
    [Header("Riding")]
    [SerializeField] private SpriteRenderer ridingRender; // ���̵� ������

    private NPCController currentNPC;
    private ItemController currentItem;
    private DoorController currentDoor;
    private Camera mainCamera;
    private float lastFireTime;
    private Vector3 startPos;
    private float timer;
    private bool isJumping;
    DialogueLine line;
    bool isItem = false;
    bool isDoor = false;
    bool isQuestFirst = false;
    private IPlayerInputStrategy currentStrategy;
    protected override void Awake()
    {
        base.Awake();

        var existing = FindObjectsOfType<PlayerController>();
        if (existing.Length > 1)
        {
            Destroy(gameObject); // �̹� �ϳ� �ִٸ� �ڽ� ����
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    protected override void Update()
    {
        base.Update();
        currentStrategy?.HandleUpdate(this);
    }
    public void Interact()
    {

        if (isDoor)
        {
            currentDoor.DoorOpen();
            currentDoor.DoorPanel.SetActive(false);
            return;
        }
        if (!isItem && !isQuestFirst)
        {
            DialogueManager.Instance.ShowDialogue(currentNPC.DialoguePanel, currentNPC.MessageText, line.firstMeeting);
            isQuestFirst = true;
        }
        else if(!isItem && isQuestFirst)
        {
            DialogueManager.Instance.ShowDialogue(currentNPC.DialoguePanel, currentNPC.MessageText, line.isQuesting);
        }
        else
        {
            UIManager.Instance.ChangeItemSlot(currentItem.Item);
            currentItem.OpenChest();
        }
    }
    protected override void FixedUpdate()
    {
        currentStrategy?.HandleFixedUpdate(this);
    }
    public void SetStrategy(IPlayerInputStrategy strategy)
    {
        currentStrategy = strategy;
    }
    public void PlayerPositionReset()
    {
        Vector3 newPos = new Vector3(0f, 0f, 0f); // �÷��̾�� ���� z = 0
        transform.rotation = Quaternion.identity;
        transform.position = newPos;
    }
    public void ForMiniGameJump()
    {
        _rd.gravityScale = 1; // �߷� ��������
        Vector2 velocity = _rd.velocity;
        velocity.x = 3f;       // x ���� ���� �ӵ�
        velocity.y = 5f;       // ����
        _rd.velocity = velocity;

        float angle = Mathf.Clamp(_rd.velocity.y * 10f, -90f, 90f);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    public void Move()
    {
        _rd.gravityScale = 0;
        Movement(movementDirection);
    }
    protected override void Rotate(Vector2 direction)
    {
        base.Rotate(direction); // ���� ȸ�� ó��

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        if (ridingRender != null)
        {
            ridingRender.flipX = isLeft;
        }
    }
    protected override void HandleAction()
    {
        // Ű���� �Է��� ���� �̵� ���� ��� (��/��/��/��)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // ���� ���� ����ȭ (�밢���� �� �ӵ� ����)
        movementDirection = new Vector2(horizontal, vertical).normalized;

        if (movementDirection != Vector2.zero)
            lookDirection = movementDirection;
    }

    public void TryShoot()
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
    public void StartJump()
    {
        if (!isJumping)
        {
            StartCoroutine(Jump());
        }
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
                if (!isQuestFirst)
                {
                    ItemManager.Instance.SpawnChest();
                }
                else
                {
                    DialogueManager.Instance.ShowDialogue(currentNPC.DialoguePanel, currentNPC.MessageText, line.isQuesting);
                }
                if (isItem)
                {
                    currentNPC.NPCCompleted();
                    isItem = false;
                }
            }
            return;
        }
        if(collision.CompareTag("Item"))
        {
            ItemController item = collision.gameObject.GetComponent<ItemController>();
            currentItem = item; 

            isItem = true;
        }
        if (collision.CompareTag("Coin"))
        {
            UIManager.coinCount++;
            UIManager.Instance.CoinUpdate();
        }
        if (collision.CompareTag("Door"))
        {
            DoorController door = collision.gameObject.GetComponent<DoorController>();
            currentDoor = door;
            currentNPC = null; 
            isItem = false;  
            line = null;
            isDoor = true;
            door.DoorPanel.SetActive(true);
        }

        if (collision.CompareTag("Obstacle"))
        {
            UIManager.Instance.GameOver();
        }

        if (collision.CompareTag("Score"))
        {
            UIManager.Instance.AddScore();
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
            }
        }
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Door"))
        {
            DoorController door = collision.gameObject.GetComponent<DoorController>();
            isDoor = false;
        }
    }

}