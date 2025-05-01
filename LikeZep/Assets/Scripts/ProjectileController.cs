using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;
    [SerializeField] private float projectileDuration = 1f;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private int damage = 1;
    [SerializeField] private bool applyKnockback = false;
    [SerializeField] private float knockbackPower = 3f;
    [SerializeField] private float knockbackTime = 0.2f;
    private float currentDuration;
    private Vector2 direction;
    private bool isReady = false;
    private bool isPlayerProjectile = true;
    private Rigidbody2D _rigidbody;
    private Transform pivot;

    ProjectileMananger projectileMananger;
    public bool fxOnDestory = true;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0); // 총알이 시각적으로 회전해야 할 경우 자식 오브젝트로 회전
    }

    private void Update()
    {
        if (!isReady) return;

        currentDuration += Time.deltaTime;

        if (currentDuration >= projectileDuration)
        {
            DestroyProjectile(transform.position, true);
            return;
        }

        _rigidbody.velocity = direction * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LayerMask levelCollisionLayer = LayerMask.GetMask("Ground", "Wall", "Level");
        if (((1 << collision.gameObject.layer) & levelCollisionLayer) != 0)
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * 0.2f, true);
        }
        if (isPlayerProjectile && collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            DamageTarget(collision);
        }
        // 적이 쐈으면 Player를 공격
        else if (!isPlayerProjectile && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            DamageTarget(collision);
        }
    }
    private void DamageTarget(Collider2D collision)
    {
        
        DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
    }
    public void Init(Vector2 dir, ProjectileMananger projectileMananger, bool isPlayer = true)
    {
        this.projectileMananger = projectileMananger;
        direction = dir.normalized;
        currentDuration = 0f;
        isReady = true;

        transform.right = direction;

        // 시각적 방향 보정 (필요할 때만)
        if (pivot != null)
        {
            if (direction.x < 0)
                pivot.localRotation = Quaternion.Euler(180, 0, 0);
            else
                pivot.localRotation = Quaternion.identity;
        }
    }

    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if(createFx)
        {
            projectileMananger.CreateImapctParticlesAtPosition(position);
        }
        Destroy(gameObject);
    }
}
