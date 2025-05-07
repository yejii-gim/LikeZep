using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : BaseManager<UIManager>
{
    [SerializeField] private Image itemSlot;
    [SerializeField] private Button riding;
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private GameObject coinPrefab;
    [Header("Clothes")]
    [SerializeField] private GameObject clothesPanel;
    [SerializeField] private GameObject needCoinPanel;
    [SerializeField] private UIClothes clothesUI;
    [Header("Riding")]
    [SerializeField] private GameObject ridingPanel;
    [SerializeField] public GameObject charcterRiding;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float rideOffsetY = 0.2f;
    [Header("MiniGame")]
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] TMP_Text scoreText; // 가운데 크게 보여지는 점수
    [SerializeField] TMP_Text bestScoreText; // 최고 점수
    [SerializeField] TMP_Text currentScoreText; // 현재 점수
    [Header("UI")]
    [SerializeField] GameObject mainUI;
    [SerializeField] GameObject miniUI;
    [Header("Ranking")]
    [SerializeField] GameObject rankingUI;
    [SerializeField] TMP_Text[] rankingScore;

    public static int coinCount = 0;
    public static int minigameScore = 0;
    private bool ridingActive = false;

    private int currentScore;
    private int bestScore = 0;
    
    protected override void Awake()
    {
        base.Awake();
        currentScore = 0;
        scoreText.text = currentScore.ToString();
        coinText.text = coinCount.ToString();
    }

    public void ChangeItemSlot(ItemData item)
    {
        Sprite itemSprite = item.icon;

        if (itemSprite != null)
        {
            itemSlot.sprite = itemSprite;
        }
    }

    public void ClearItemSlot()
    {
        itemSlot.sprite = null;
    }

    public void CoinSpawn(Transform position)
    {
        Instantiate(coinPrefab, position.position, Quaternion.identity);
    }

    public void CoinSpawn(Vector3 position)
    {
        Instantiate(coinPrefab, position, Quaternion.identity);
    }

    public void CoinUpdate()
    {
        coinText.text = coinCount.ToString();
    }

    public void TogglePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void ToggleClothesPanel() => TogglePanel(clothesPanel);
    public void ToggleRidingPanel() => TogglePanel(ridingPanel);
    public void ToggleNeedCoinPanel() => TogglePanel(needCoinPanel);
    public void ToggleRiding()
    {
        ridingActive = charcterRiding.activeSelf;
        StartCoroutine(SmoothRidingTransition(!ridingActive));
    }

    // 라이딩할때 효과넣어주는 코루틴
    private IEnumerator SmoothRidingTransition(bool riding)
    {
        float duration = 0.2f;
        float elapsed = 0f;
        float offset = riding ? rideOffsetY : -rideOffsetY;

        Vector3 startPos = playerTransform.position;
        Vector3 targetPos = startPos + new Vector3(0f, offset, 0f);

        if (riding) charcterRiding.SetActive(true);
        if (!riding) charcterRiding.SetActive(false);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            playerTransform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        playerTransform.position = targetPos;
    }

    public void GameOver()
    {
        scoreText.text = "0";
        GameOverPanel.SetActive(true);
        currentScoreText.text = currentScore.ToString();

        SaveManager.Instance.SaveScoreRanking(currentScore);
        UpdateBestScoreUI();
  
        Time.timeScale = 0;
    }
    // 베스트 스코어 업데이트
    private void UpdateBestScoreUI()
    {
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            bestScoreText.text = bestScore.ToString();
        }
    }
    public void RestartButton() => LoadSceneWithReset(1);
    public void GameOverExitButton() => LoadSceneWithReset(0);

    private void LoadSceneWithReset(int sceneIndex)
    {
        currentScore = 0;
        scoreText.text = currentScore.ToString();
        GameOverPanel.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex);

        if (sceneIndex == 0)
            OpenMainUI();
        else
            OpenMiniUI();
    }

    public void AddScore()
    {
        currentScore++;
        scoreText.text = currentScore.ToString();
    }

    public void OpenMainUI()
    {
        mainUI.SetActive(true);
        miniUI.SetActive(false);
    }

    public void OpenMiniUI()
    {
        mainUI.SetActive(false);
        miniUI.SetActive(true);
    }

    public void OpenRankingButton()
    {
        for (int i = 0; i < SaveManager.MaxRank; i++)
        {
            int score = PlayerPrefs.GetInt("HighScore" + i, 000);
            rankingScore[i].text = $"{i + 1}. {score}\n";
        }
        rankingUI.SetActive(true);
    }

    public void CloseRankingButton()
    {
        rankingUI.SetActive(false);
    }
}
