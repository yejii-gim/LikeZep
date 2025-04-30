using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    [SerializeField] private int attackRange;
    [SerializeField] private GameObject enemyProjectilePrefab;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float shootRange = 10f;

    private float lastFireTime;
    private EnemyManager enemeyManager;
    private Transform target;
    
    [SerializeField] private float followRange = 15f;
    
    public void Init(EnemyManager enemyManager, Transform target)
    {
       this.enemeyManager = enemyManager;
       this.target = target;
    }
    
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }

    protected override void HandleAction()
    {
        base.HandleAction();
        if (target == null) return;

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        if (distance <= followRange)
        {
            lookDirection = direction;

            if (distance < shootRange)
            {
    
            }

            if (distance > attackRange)
            {
                movementDirection = direction;
            }
            else
            {
                movementDirection = Vector2.zero;
            }
        }

    }
    private void TryShoot()
    {
        if (Time.time >= lastFireTime + fireRate)
        {
            //direction = (target.position - transform.position).normalized;
            ProjectileMananger.Instance.ShootBullet(0, transform.position, DirectionToTarget(), false);
            lastFireTime = Time.time;
        }
    }
    public override void Death()
    {
        base.Death();
        EnemyManager.Instance.RemoveEnemyOnDeath(this);
    }
}
