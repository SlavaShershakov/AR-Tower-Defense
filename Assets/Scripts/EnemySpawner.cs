using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float delayTime;
    [SerializeField] private float repeatTime;
    [SerializeField] private float radius;

    private Vector3 SpawnPosition
    {
        get 
        {
            Vector2 insideUnitCirclePosition = Random.insideUnitCircle;
            return Vector3.Normalize(new Vector3(insideUnitCirclePosition.x, 0.0f, insideUnitCirclePosition.y)) * radius;
        }
    }

    public void StartSpawn()
    {
        StartCoroutine(SpawningRoutine());
    }
    private IEnumerator SpawningRoutine()
    {
        yield return new WaitForSeconds(delayTime);

        while (true)
        {
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], SpawnPosition, Quaternion.identity, transform);
            yield return new WaitForSeconds(repeatTime);
        }
    }
}
