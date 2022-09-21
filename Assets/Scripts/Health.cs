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
    public bool isAlive { get; private set; } = true;

    private void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.slider.value = healthBar.slider.maxValue = maxHealth = health;
    }
    public void RecieveDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            isAlive = false;
            Destroy(gameObject);
        }

        healthBar.slider.value = health;
    }
}