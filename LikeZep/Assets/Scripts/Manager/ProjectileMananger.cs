using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileMananger : BaseManager<ProjectileMananger>
{
    public static ProjectileMananger Instance;

    [SerializeField] private GameObject[] projectilePrefabs;
    [SerializeField] private float bulletSpeed;
    private int size = 10;
    private void Awake()
    {
        Instance = this;
    }

    public void ShootBullet(int index, Vector2 position, Vector2 direction, bool isPlayer)
    {
        GameObject bulletObj = Instantiate(projectilePrefabs[index], position, Quaternion.identity);
  
        // ȸ�� ���� (�ð��� ����)
        if (direction.x < 0)
            bulletObj.transform.rotation = Quaternion.Euler(0f, 180f, 0f); // ����
        else
            bulletObj.transform.rotation = Quaternion.Euler(0f, 0f, 0f);   // ������
        var proj = bulletObj.GetComponent<ProjectileController>();
        proj.Init(direction,this,isPlayer);
    }
}
