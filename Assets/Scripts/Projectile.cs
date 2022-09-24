using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int explosionDamage;
    [SerializeField] private float explosionRadius;
    private Rigidbody projectileRigidbody;

    private ObjectPool<GameObject> projectilePool;

    private void Start()
    {
        projectileRigidbody = GetComponent<Rigidbody>();
        projectilePool = ObjectPooler.Instance.FindPool<Projectile>();
    }
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(projectileRigidbody.velocity);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }
    private void Explode()
    {
        var overlappedColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var overlappedCollider in overlappedColliders)
        {
            if (overlappedCollider.GetComponent<Enemy>() is Enemy currentEnemy)
            {
                float distance = Vector3.Distance(transform.position, currentEnemy.transform.position);
                currentEnemy.GetComponent<Health>().RecieveDamage((int)((explosionRadius - distance) * explosionDamage));
            }
        }

        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        projectilePool.Release(gameObject);
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
#endif
}