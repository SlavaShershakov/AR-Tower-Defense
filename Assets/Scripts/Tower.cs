using UnityEngine;

public class Tower : MonoBehaviour
{
    private void OnDestroy()
    {
        GameManager.Instance.EndGame();
    }
}