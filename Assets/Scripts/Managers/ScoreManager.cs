using UnityEngine;
using System.Collections;

public class ScoreManager : PersistentSingleton<ScoreManager> 
{
    public int Score { get { return _score; } }
    [SerializeField]private int _score;


    public void AddScore(int count)
    {
        _score += count;
        GUIManager.Instance.RefreshScore(_score);
    }

    public void SetScore(int count)
    {
        _score = count;
        GUIManager.Instance.RefreshScore(_score);
    }


    public void SaveScore()
    {
        if(PlayerPrefs.HasKey(GameConstants.Score.HighScoreKey))
        {
            int lastHighscore = GetHighScore();
            if(_score > lastHighscore)
            {
                PlayerPrefs.SetInt(GameConstants.Score.HighScoreKey,_score);
            }
        }

    }

    public int GetHighScore()
    {
        int score = PlayerPrefs.GetInt(GameConstants.Score.ScoreKey);
        return score;   
    }
}
