using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : BaseManager<GameManager>
{
    [SerializeField] private PlayerController player;

    private IPlayerInputStrategy defaultStrategy = new DefaultMoveStrategy();
    private IPlayerInputStrategy miniGameStrategy = new MiniGameStrategy(); 

    private void Start()
    {
        SetDefaultMode();
        DialogueManager.Instance.Init();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CameraController cam = FindObjectOfType<CameraController>();
        cam?.ResetCameraPosition();
        if (player == null)
            player = FindObjectOfType<PlayerController>();
        player.PlayerPositionReset();
        if (scene.name == "FlappyBird")
        {
            SetMiniGameMode(cam);
            player.SetStrategy(miniGameStrategy);
        }
        else if (scene.name == "MainScene")
        {
            SetDefaultMode(cam);
            player.SetStrategy(defaultStrategy);
        }
    }
    public void SetDefaultMode(CameraController cam = null)
    {
        UIManager.Instance.OpenMainUI();
        DialogueManager.Instance.Init();
        player.SetStrategy(defaultStrategy);
        cam?.SetMiniGameMode(false);
    }
    
    public void SetMiniGameMode(CameraController cam = null)
    {
        UIManager.Instance.OpenMiniUI();
        BGLooper looper = GameObject.FindObjectOfType<BGLooper>();
        looper?.Init();
        
        player.SetStrategy(miniGameStrategy);
        cam?.SetMiniGameMode(true);
    }
}
