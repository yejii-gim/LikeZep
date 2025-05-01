using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseManager<GameManager>
{
    public static GameManager Instance;

    public PlayerController player { get; private set; }

    [SerializeField] private int currentWaveIndex = 0;

    private EnemyManager enemyManager;
}
