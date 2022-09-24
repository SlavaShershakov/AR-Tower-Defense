using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private List<ObjectPool> objectPools = new List<ObjectPool>();
    public static ObjectPooler Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        foreach (var objectPool in objectPools)
        {
            objectPool.Start();
        }
    }
    public ObjectPool<GameObject> FindPool(string pooledObjectName)
    {
        return objectPools.Find(pooledObject => pooledObject.pooledObject.name + "(Clone)" == pooledObjectName || pooledObject.pooledObject.name == pooledObjectName).Pool;
    }
    public ObjectPool<GameObject> FindPool<T>()
    {
        return FindPool(typeof(T).Name);
    }
    public List<ObjectPool<GameObject>> FindPools<T>()
    {
        List<ObjectPool<GameObject>> findedPools = new List<ObjectPool<GameObject>>();

        foreach (var objectPool in objectPools)
        {
            if (objectPool.pooledObject.TryGetComponent(out T component))
            {
                findedPools.Add(objectPool.Pool);
            }
        }

        return findedPools;
    }

    [System.Serializable]
    private class ObjectPool
    {
        public GameObject pooledObject;
        public int defaultCapacity;
        private GameObject poolParent;
        public ObjectPool<GameObject> Pool { get; private set; }

        public void Start()
        {
            poolParent = new GameObject(pooledObject.name + " Pool");
            poolParent.transform.SetParent(Instance.transform);

            Pool = new ObjectPool<GameObject>(CreatePooledItem, defaultCapacity: defaultCapacity);
        }
        private GameObject CreatePooledItem()
        {
            return Instantiate(pooledObject, poolParent.transform);
        }
    }
}