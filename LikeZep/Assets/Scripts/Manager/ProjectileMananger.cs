using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileMananger : BaseManager<ProjectileMananger>
{

    [SerializeField] private GameObject[] projectilePrefabs;
    [SerializeField] private float bulletSpeed;
    private int size = 10;

    public void ShootBullet(int index, Vector2 position, Vector2 direction, bool isPlayer)
    {
        GameObject bulletObj = Instantiate(projectilePrefabs[index], position, Quaternion.identity);
  
        // 회전 설정 (시각적 방향)
        if (direction.x < 0)
            bulletObj.transform.rotation = Quaternion.Euler(0f, 180f, 0f); // 왼쪽
        else
            bulletObj.transform.rotation = Quaternion.Euler(0f, 0f, 0f);   // 오른쪽
        var proj = bulletObj.GetComponent<ProjectileController>();
        proj.Init(direction,this,isPlayer);
    }
}
