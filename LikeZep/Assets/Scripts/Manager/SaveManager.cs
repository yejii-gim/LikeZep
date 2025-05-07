using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : BaseManager<SaveManager>
{
    public const int MaxRank = 5;
    private const string HighScoreKeyPrefix = "HighScore";

    // 스코어 저장 
    public void SaveScoreRanking(int score)
    {
        List<int> scores = new List<int>();

        for (int i = 0; i < MaxRank; i++)
        {
            scores.Add(PlayerPrefs.GetInt("HighScore" + i, 0));
        }

        scores.Add(score);
        scores.Sort((a, b) => b.CompareTo(a));
        scores = scores.GetRange(0, MaxRank);

        // 다시 저장
        for (int i = 0; i < MaxRank; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, scores[i]);
        }

        PlayerPrefs.Save();
    }

    public List<int> LoadScores()
    {
        var scores = new List<int>();
        for (int i = 0; i < MaxRank; i++)
            scores.Add(PlayerPrefs.GetInt(HighScoreKeyPrefix + i, 0));

        return scores;
    }
}
