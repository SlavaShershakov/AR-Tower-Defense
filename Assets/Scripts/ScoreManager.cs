using UnityEngine;
using TMPro;
using System.IO;

public static class ScoreManager
{
    [SerializeField] static private TextMeshProUGUI scoreText;
    [SerializeField] static private TextMeshProUGUI highScoreText;

    private static int score;
    private static int Score
    {
        get => score;
        set
        {
            score = value;
            scoreText.text = "SCORE: " + score;

            if (score > highScore)
            {
                HighScore = score;
                SaveHighScore();
            }
        }
    }

    private static int highScore;
    private static int HighScore
    {
        get => highScore;
        set
        {
            highScore = value;
            highScoreText.text = "HIGHSCORE: " + highScore;
        }
    }

    public static void Start()
    {
        scoreText = GameObject.Find("ScoreText (TMP)").GetComponent<TextMeshProUGUI>();
        highScoreText = GameObject.Find("HighScoreText (TMP)").GetComponent<TextMeshProUGUI>();
        LoadHighScore();
        Score = 0;
    }
    public static void AddPoints(int points)
    {
        Score += points;
    }

    [System.Serializable]
    private class SaveData
    {
        public int highScore;
    }
    private static void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScore = HighScore;

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", JsonUtility.ToJson(data));
    }
    private static void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
            HighScore = data.highScore;
        }
        else
        {
            HighScore = 0;
        }
    }
}