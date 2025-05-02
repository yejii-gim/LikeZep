using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : BaseManager<GameManager>
{
    public static GameManager Instance;
    [SerializeField] private PlayerController player;

    private IPlayerInputStrategy defaultStrategy = new DefaultMoveStrategy();
    private IPlayerInputStrategy miniGameStrategy = new MiniGameStrategy(); // 예시 전략
   
    private void Start()
    {
        DialogueManager.Instance.Init();
        SetDefaultMode();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (player == null)
            player = FindObjectOfType<PlayerController>(); // 혹시라도 씬 전환으로 player가 null이면 찾아줌

        if (scene.name == "FlappyBird")
        {
            player.SetStrategy(miniGameStrategy);
        }
        else if (scene.name == "MainScene")
        {
            player.SetStrategy(defaultStrategy);
        }
    }
    public void SetDefaultMode()
    {
        player.SetStrategy(defaultStrategy);
    }
    
    public void SetMiniGameMode()
    {
        player.SetStrategy(miniGameStrategy);
    }
}
