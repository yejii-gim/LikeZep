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
        // Collision Layer 오브젝트에 부딪혔을 경우 혹은 일정 시간 경과시 방향 전환
        if(Time.time >= nextChangeTime || IsBlocked())
        {
            PickNewDirection();
            nextChangeTime = Time.time + changeTime;
        }

        if(movementDirection != Vector2.zero)
            lookDirection = movementDirection;
    }

    // Collsion 레이어 만난 경우 Blocked
    private bool IsBlocked()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movementDirection,0.5f,collisionLayer);
        return hit.collider != null;
    }

    // 방향 무작위 설정
    private void PickNewDirection()
    { 
        int angle = Random.Range(0, 8) * 45;
        float rad = angle * Mathf.Deg2Rad;
        movementDirection = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
    }

    // 플레이어 쳐다보게 하기
    protected Vector2 DirectionToTarget(Transform target)
    {
        return (target.position - transform.position).normalized;
    }

    // 내 찌르기 오브젝트 만났을때 멈추기
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
            enabled = false; // 멈춤
        }
    }
}
