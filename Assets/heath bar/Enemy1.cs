using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    private HealthSystem healthSystem;

    [SerializeField] private int startingHealth = 50;
    [SerializeField] private int contactDamage = 10;
    [SerializeField] private HealthBar healthBar;  // 👈 Reference to health bar UI

    void Start()
    {
        healthSystem = new HealthSystem(startingHealth);
        healthBar.SetMaxHealth(startingHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log($"{gameObject.name} hit Player!");
            player.TakeHit(contactDamage);
        }
    }

    public void TakeHit(int damage)
    {
        healthSystem.TakeDamage(damage);
        healthBar.SetHealth(healthSystem.GetHealth());

        if (healthSystem.IsDead())
        {
            Debug.Log($"{gameObject.name} defeated!");
            Destroy(gameObject);
        }
    }
}