using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    private GameObject attackTarget;
    private Health attackTargetHealth;
    private Health enemyHealth;
    private float maxSpeed;
    private int scorePoints;

    private Animator enemyAnimator;
    private AudioSource enemyAudio;

    private ObjectPool<GameObject> enemyPool;

    private void Awake()
    {
        maxSpeed = speed;
    }
    private void Start()
    {
        attackTarget = Tower.Instace.gameObject;
        transform.LookAt(attackTarget.transform);

        attackTargetHealth = attackTarget.GetComponent<Health>();
        enemyHealth = GetComponent<Health>();
        scorePoints = enemyHealth.MaxHealth;

        enemyAnimator = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();

        enemyPool = ObjectPooler.Instance.FindPool(gameObject.name);
    }
    private void OnEnable()
    {
        if (attackTarget != null)
            transform.LookAt(attackTarget.transform);
        
        speed = maxSpeed;
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
                enemyAudio.Play();
                attackTargetHealth.RecieveDamage(damage);
                yield return new WaitForSeconds(damageDealingTime / 2);

                gameObject.SetActive(false);
            }
        }
    }
    private void OnDisable()
    {
        enemyPool.Release(gameObject);

        if (!enemyHealth.isAlive)
            ScoreManager.AddPoints(scorePoints);
    }
}