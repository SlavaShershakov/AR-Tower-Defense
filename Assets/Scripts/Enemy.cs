using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    private GameObject attackTarget;

    private void Start()
    {
        attackTarget = GameObject.Find("Tower");
        transform.LookAt(attackTarget.transform);
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == attackTarget)
        {
            attackTarget.GetComponent<Health>().RecieveDamage(damage);
            Destroy(gameObject);
        }
    }
}