using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float delayTime;
    [SerializeField] private float repeatTime;
    [SerializeField] private float radius;
    private List<ObjectPool<GameObject>> enemyPrefabsPools = new List<ObjectPool<GameObject>>();
    public static EnemySpawner Instance { get; private set; }
    private Vector3 SpawnPosition
    {
        get 
        {
            Vector2 insideUnitCirclePosition = Random.insideUnitCircle;
            return Vector3.Normalize(new Vector3(insideUnitCirclePosition.x, 0.0f, insideUnitCirclePosition.y)) * radius;
        }
    }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        enemyPrefabsPools = ObjectPooler.Instance.FindPools<Enemy>();
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
            var curretnEnemy = enemyPrefabsPools[Random.Range(0, enemyPrefabsPools.Count)].Get();
            curretnEnemy.transform.position = SpawnPosition;
            curretnEnemy.SetActive(true);
            yield return new WaitForSeconds(repeatTime);
        }
    }
}
