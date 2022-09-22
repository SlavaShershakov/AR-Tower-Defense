using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    private GameObject attackTarget;
    private Health enemyHealth;
    private int scorePoints;

    private Animator enemyAnimator;

    private void Start()
    {
        attackTarget = GameObject.Find("Tower");
        transform.LookAt(attackTarget.transform);

        enemyHealth = GetComponent<Health>();
        scorePoints = enemyHealth.MaxHealth;

        enemyAnimator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == attackTarget)
        {
            StartCoroutine(AttackingRoutine());

            IEnumerator AttackingRoutine()
            {
                speed = 0.0f;

                enemyAnimator.SetTrigger("Attack");
                float damageDealingTime = enemyAnimator.GetCurrentAnimatorStateInfo(0).length;
                yield return new WaitForSeconds(damageDealingTime / 2);
                attackTarget.GetComponent<Health>().RecieveDamage(damage);
                yield return new WaitForSeconds(damageDealingTime / 2);

                Destroy(gameObject);
            }
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