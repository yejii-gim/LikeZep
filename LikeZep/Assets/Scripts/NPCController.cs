using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NPCController : BaseController
{
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private string myStabTag;

    private float changeTime = 5;
    private float nextChangeTime;

    private void Start()
    {
        PickNewDirection();
    }

    protected override void HandleAction()
    {
        // Collision Layer ������Ʈ�� �ε����� ��� Ȥ�� ���� �ð� ����� ���� ��ȯ
        if(Time.time >= nextChangeTime || IsBlocked())
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
        if (collision.CompareTag(myStabTag))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                DirectionToTarget(player.transform);
                Rotate(lookDirection); 
            }
            movementDirection = Vector2.zero;
            enabled = false; // ����
        }
    }
}
