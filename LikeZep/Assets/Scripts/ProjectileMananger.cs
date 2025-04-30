using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileMananger : BaseManager<ProjectileMananger>
{
    public static ProjectileMananger Instance;

    [SerializeField] private GameObject[] projectilePrefabs;
    [SerializeField] private ParticleSystem impactParticleSystem;

    private int size = 10;
    private void Awake()
    {
        Instance = this;
    }

    public void ShootBullet(int index, Vector2 position, Vector2 direction, bool isPlayer)
    {
        GameObject bulletObj = Instantiate(projectilePrefabs[index], position, Quaternion.identity);
        var proj = bulletObj.GetComponent<ProjectileController>();
        proj.Init(direction,this,isPlayer);
    }

    public void CreateImapctParticlesAtPosition(Vector3 postion)
    {
        impactParticleSystem.transform.position = postion;
        ParticleSystem.EmissionModule em = impactParticleSystem.emission;
        em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(size)));
        ParticleSystem.MainModule mainModule = impactParticleSystem.main;
        mainModule.startSpeedMultiplier = 10f;
        impactParticleSystem.Play();
    }
}
