using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    private HealthBar healthBar;
    private int maxHealth;
    public int MaxHealth
    {
        get => maxHealth;
        private set => maxHealth = value;
    }
    public bool isAlive { get; private set; }

    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.slider.value = healthBar.slider.maxValue = maxHealth = health;
    }
    private void OnEnable()
    {
        isAlive = true;
        healthBar.slider.value = health = maxHealth;
    }
    public void RecieveDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            isAlive = false;
            gameObject.SetActive(false);
        }

        healthBar.slider.value = health;
    }
}