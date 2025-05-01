using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : BaseManager<GameManager>
{
    public static ItemManager Instance;
    [SerializeField] GameObject chestPrefab;
    [SerializeField] private Vector2 spawnAreaMin = new Vector2(-10, -5);
    [SerializeField] private Vector2 spawnAreaMax = new Vector2(10, 5);
    
    private void Awake()
    {
        Instance = this;
    }

    // 랜덤으로 아이템 들은 보물상자 나오게x 
    public void SpawnChest()
    {
        Vector2 randomPos = new Vector2(Random.Range(spawnAreaMin.x, spawnAreaMax.x),Random.Range(spawnAreaMin.y, spawnAreaMax.y) );

        Instantiate(chestPrefab, randomPos, Quaternion.identity);
    }

   
}
