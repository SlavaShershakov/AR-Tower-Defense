using UnityEngine;

public class Tower : MonoBehaviour
{
    public static Tower Instace { get; private set; }

    private void Awake()
    {
        Instace = this;
    }
    private void OnDisable()
    {
        GameManager.Instance.EndGame();
    }
}