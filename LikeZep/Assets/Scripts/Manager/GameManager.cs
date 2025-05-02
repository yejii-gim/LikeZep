using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : BaseManager<GameManager>
{
    public static GameManager Instance;
    [SerializeField] private PlayerController player;

    private IPlayerInputStrategy defaultStrategy = new DefaultMoveStrategy();
    private IPlayerInputStrategy miniGameStrategy = new MiniGameStrategy(); // ���� ����
   
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
            player = FindObjectOfType<PlayerController>(); // Ȥ�ö� �� ��ȯ���� player�� null�̸� ã����

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
