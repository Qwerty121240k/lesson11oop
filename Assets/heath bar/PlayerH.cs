using UnityEngine;

public class PlayerH : MonoBehaviour
{
    private HealthSystem healthSystem;

    [SerializeField] private int startingHealth = 100;
    [SerializeField] private int contactDamage = 20;
    [SerializeField] private HealthBar healthBar;  // 👈 Reference to the UI health bar

    void Start()
    {
        // Initialize health system
        healthSystem = new HealthSystem(startingHealth);
        healthBar.SetMaxHealth(startingHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            Debug.Log("Player hit Enemy!");
            enemy.TakeHit(contactDamage);
        }
    }

    public void TakeHit(int damage)
    {
        healthSystem.TakeDamage(damage);
        healthBar.SetHealth(healthSystem.GetHealth());

        if (healthSystem.IsDead())
        {
            Debug.Log("Player has died!");
        }
    }
}