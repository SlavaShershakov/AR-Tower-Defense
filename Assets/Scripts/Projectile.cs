using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody projectileRigidbody;

    private void Start()
    {
        projectileRigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(projectileRigidbody.velocity);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
