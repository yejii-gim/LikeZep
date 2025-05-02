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
        SetDefaultMode();
        DialogueManager.Instance.Init();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CameraController cam = FindObjectOfType<CameraController>();
        cam?.ResetCameraPosition();
        if (player == null)
            player = FindObjectOfType<PlayerController>(); // 혹시라도 씬 전환으로 player가 null이면 찾아줌
        player.PlayerPositionReset();
        if (scene.name == "FlappyBird")
        {
            SetMiniGameMode();
            cam?.SetMiniGameMode(true);
            player.SetStrategy(miniGameStrategy);
        }
        else if (scene.name == "MainScene")
        {
            SetDefaultMode();
            cam?.SetMiniGameMode(false);
            player.SetStrategy(defaultStrategy);
        }
    }
    public void SetDefaultMode()
    {
        UIManager.Instance.openMainUI();
        player.SetStrategy(defaultStrategy);
    }
    
    public void SetMiniGameMode()
    {
        UIManager.Instance.openMiniUI();
        BGLooper looper = GameObject.FindObjectOfType<BGLooper>();
        looper?.Init();
        
        player.SetStrategy(miniGameStrategy);
    }
}
