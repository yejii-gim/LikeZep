using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class NPCController : BaseController
{
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private string myStabTag;
    [Header("Dialogue")]
    [SerializeField] private GameObject talkPanel;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI messageText;
    public GameObject TalkPanel{get => talkPanel;}
    public GameObject DialoguePanel { get => dialoguePanel;  }
    public TextMeshProUGUI MessageText { get => messageText; }

    private float changeTime = 5f;
    private float nextChangeTime;
    private bool isStabbed = false;
    private bool isActive = false;
    protected override void Start()
    {
        NPCManager.Instance.RegisterNPC(this);
    }
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void FixedUpdate()
    {
        if (!isActive) return;
        if (!isStabbed)
            Movement(movementDirection);
        else
            animationHandler.Stop();
    }
    public void Activate()
    {
        isActive = true;
        PickNewDirection();
    }
    protected override void HandleAction()
    {
        if (!isActive || isStabbed) return;
        // Collision Layer ������Ʈ�� �ε����� ��� Ȥ�� ���� �ð� ����� ���� ��ȯ
        if (Time.time >= nextChangeTime || IsBlocked())
        {
            PickNewDirection();
            nextChangeTime = Time.time + changeTime;
        }

        if(movementDirection != Vector2.zero)
            lookDirection = movementDirection;
    }

    // Collsion ���̾� ���� ��� Blocked
    private bool IsBlocked()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movementDirection,0.5f,collisionLayer);
        return hit.collider != null;
    }

    // ���� ������ ����
    private void PickNewDirection()
    { 
        int angle = Random.Range(0, 8) * 45;
        float rad = angle * Mathf.Deg2Rad;
        movementDirection = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
    }

    // �÷��̾� �Ĵٺ��� �ϱ�
    protected Vector2 DirectionToTarget(Transform target)
    {
        return (target.position - transform.position).normalized;
    }

    // �� ��� ������Ʈ �������� ���߱�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(myStabTag) && !isStabbed)
        {
            isStabbed = true;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                DirectionToTarget(player.transform);
                Rotate(lookDirection);
            }

            if (_rd != null)
                _rd.velocity = Vector2.zero;

            movementDirection = Vector2.zero;

            animationHandler.Stop();
        }
        if (collision.CompareTag("Player"))
        {
            talkPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            talkPanel.SetActive(false);
        }
    }

    // ����Ʈ �Ϸ�� npc ����
    public void NPCCompleted()
    {
        NPCManager.Instance.NotifyNPCCompleted(this);
        UIManager.Instance.ClearItemSlot();
        Destroy(gameObject,1);
    }
    
}
