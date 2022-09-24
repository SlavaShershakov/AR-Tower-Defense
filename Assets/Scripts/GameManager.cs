using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject gameOverPanel;
    private bool isGameActive;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gameOverPanel = GameObject.Find("GameOverPanel");
        gameOverPanel.SetActive(false);
        
        ScoreManager.Start();
    }
    public void StartGame()
    {
        if (isGameActive)
            return;

        isGameActive = true;
        EnemySpawner.Instance.StartSpawn();
    }
    public void EndGame()
    {
        isGameActive = false;
        gameOverPanel.SetActive(true);
        EnemySpawner.Instance.gameObject.SetActive(false);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}