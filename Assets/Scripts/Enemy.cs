using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    private GameObject attackTarget;

    private void Start()
    {
        attackTarget = GameObject.Find("ImageTarget");
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
            Destroy(gameObject);
        }
    }
}