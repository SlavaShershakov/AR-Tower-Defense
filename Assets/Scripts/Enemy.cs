using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    private GameObject attackTarget;
    private Health enemyHealth;
    private int scorePoints;

    private void Start()
    {
        attackTarget = GameObject.Find("Tower");
        transform.LookAt(attackTarget.transform);

        enemyHealth = GetComponent<Health>();
        scorePoints = enemyHealth.MaxHealth;
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
    private void OnDestroy()
    {
        if (!enemyHealth.isAlive)
        {
            ScoreManager.AddPoints(scorePoints);
        }
    }
}