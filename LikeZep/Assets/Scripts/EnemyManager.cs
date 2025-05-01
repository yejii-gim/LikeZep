using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : BaseManager<EnemyManager>
{
    public static EnemyManager Instance;

    private Coroutine waveRoutine;

    [SerializeField] private List<GameObject> enemyPrefab;
    [SerializeField] private List<Rect> spawnAreas;
    [SerializeField] private Color gizmeoColor = new Color(1, 0, 0, 0.3f);
    private List<EnemyController> activeEnemies = new List<EnemyController>();

    private bool enemySpawnComplite;

    [SerializeField] private float timeBetweenSpawn = 0.2f;
    [SerializeField] private float timeBetweenWaves = 1f;
    GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void StartWave(int waveCount)
    {
        if(waveRoutine != null)
        {
            StopCoroutine(waveRoutine);
        }
        waveRoutine = StartCoroutine(SpawnWave(waveCount));
    }

    public void StopWave()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnWave(int waveCount)
    {
        enemySpawnComplite = false;
        yield return new WaitForSeconds(timeBetweenSpawn);

        for(int i = 0; i<waveCount; i++)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            SpawnRandomEnemy();
        }

        enemySpawnComplite = true;
    }

    private void SpawnRandomEnemy()
    {
        if(enemyPrefab.Count == 0 || spawnAreas.Count == 0)
        {
            Debug.LogWarning("Enemy Prefabs 또는 Spawn Areas가 설정되어있지 않습니다.");
            return;
        }

        GameObject randomPrefab = enemyPrefab[Random.Range(0, enemyPrefab.Count)];

        Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];

        Vector2 randomPosition = new Vector2(
            Random.Range(randomArea.xMin, randomArea.xMax),
            Random.Range(randomArea.yMin, randomArea.yMax));    

        GameObject spawnEnemy = Instantiate(randomPrefab, new Vector3(randomPosition.x, randomPosition.y),Quaternion.identity);
        EnemyController enemyController = spawnEnemy.GetComponent<EnemyController>();
        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
        {
            enemyController.Init(this,gameManager.player.transform);
            activeEnemies.Add(enemyController);
        }
        else
        {
            Debug.LogError("Player를 찾을 수 없습니다. Enemy Init 실패");
            Destroy(spawnEnemy);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (spawnAreas == null) return;

        Gizmos.color = gizmeoColor;
        foreach(var area in spawnAreas)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);

            Gizmos.DrawCube(center, size);
        }
    }

    public void RemoveEnemyOnDeath(EnemyController enemy)
    {
        activeEnemies.Remove(enemy);
    }
}
