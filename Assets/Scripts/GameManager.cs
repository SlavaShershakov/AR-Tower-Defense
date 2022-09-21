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
        gameOverPanel.SetActive(true);
        Destroy(EnemySpawner.Instance.gameObject);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}