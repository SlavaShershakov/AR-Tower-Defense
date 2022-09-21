using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    private HealthBar healthBar;

    private void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.slider.value = healthBar.slider.maxValue = health;
    }
    public void RecieveDamage(int damage)
    {
        health -= damage;

        if (health <= 0) 
            Destroy(gameObject);

        healthBar.slider.value = health;
    }
}