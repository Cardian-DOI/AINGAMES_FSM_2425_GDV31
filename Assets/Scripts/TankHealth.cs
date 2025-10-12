﻿using UnityEngine;

public class TankHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;

    [SerializeField]
    private GameObject deathEffect; // Explosion prefab to spawn on death

    [SerializeField]
    private float explosionSpread = 3f; // Distance between repeated explosions
    [SerializeField]
    private int explosionCount = 5; // How many extra explosions to spawn

    private bool isDead = false;
    
    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Function to take damage
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death();
        }
    }

    private void Death()
    {
        if (isDead) return;
        isDead = true;

        // Spawn central explosion at tank position
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        // Spawn multiple nearby explosions
        for (int i = 0; i < explosionCount; i++)
        {
            Vector3 offset = new Vector3(
                Random.Range(-explosionSpread, explosionSpread),
                0,
                Random.Range(-explosionSpread, explosionSpread)
            );

            Instantiate(deathEffect, transform.position + offset, Quaternion.identity);
        }

        // Destroy the tank after a short delay
        Destroy(gameObject, 0.3f);
    }
}
